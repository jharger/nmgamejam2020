using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using Random = UnityEngine.Random;

public class EyeballController : MonoBehaviour {
    [SerializeField] private float torqueFactor = 45f;

    [SerializeField] private float jumpFactor = 50f;

    [SerializeField] private float shootFactor = 50f;

    [SerializeField] private float groundForceFactor = 5f;

    [SerializeField] private float airForceFactor = 5f;

    [SerializeField] private float groundRayDistance = 1f;

    [SerializeField] private AudioClip[] squishClips;

    [SerializeField] private AudioSource squishSource;

    [SerializeField] private float minSquishTime = 0.25f;
    [SerializeField] private float maxSquishTime = 0.5f;

    private Rigidbody _rigidbody;
    private Rigidbody[] _jumpBodies;
    private Vector3 _torque = Vector3.zero;
    private Vector3 _force = Vector3.zero;
    private bool _wantsJump = false;
    private float squishCountdown = 0f;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
        _jumpBodies = GetComponentsInChildren<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update() {
        var viewCamera = Camera.main;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        _wantsJump = Input.GetButton("Jump");

        if (viewCamera) {
            _torque = viewCamera.transform.right * v - viewCamera.transform.forward * h;
            _force = viewCamera.transform.forward * v + viewCamera.transform.right * h;
        }
    }

    private void FixedUpdate() {
        if (_rigidbody.isKinematic) {
            return;
        }

        var ray = new Ray(transform.position, Vector3.down);
        bool onGround = Physics.Raycast(ray, out var hit, groundRayDistance);
        if (onGround && _wantsJump) {
            foreach (var jumpBody in _jumpBodies) {
                jumpBody.AddForce(Vector3.up * (jumpFactor * jumpBody.mass), ForceMode.Impulse);
            }
        }

        if (onGround) {
            if (_torque.sqrMagnitude > 0.1f) {
                squishCountdown -= Time.fixedDeltaTime;
                if (squishCountdown <= 0f) {
                    PlaySquish();
                    squishCountdown = Random.Range(minSquishTime, maxSquishTime);
                }
            }
            _rigidbody.AddTorque(_torque * torqueFactor);
            _rigidbody.AddForce(_force * groundForceFactor);
        }
        else {
            _rigidbody.AddForce(_force * airForceFactor);
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundRayDistance);
    }

    private void OnCollisionEnter(Collision other) {
        PlaySquish();
    }

    private void PlaySquish() {
        int clipIndex = Random.Range(0, squishClips.Length);
        var clip = squishClips[clipIndex];

        squishSource.PlayOneShot(clip);
    }

    public void FreezeEyeball() {
        _rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
        _rigidbody.isKinematic = true;
    }

    public void UnfreezeEyeball() {
        _rigidbody.isKinematic = false;
        _rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    public void Shoot(Vector3 direction) {
        foreach (var jumpBody in _jumpBodies) {
            jumpBody.AddForce(direction.normalized * shootFactor, ForceMode.Impulse);
        }
    }
}
