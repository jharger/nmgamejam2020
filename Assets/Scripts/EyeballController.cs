using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class EyeballController : MonoBehaviour {
    [SerializeField] private float torqueFactor = 45f;

    [SerializeField] private float jumpFactor = 50f;

    [SerializeField] private float groundForceFactor = 5f;

    [SerializeField] private float airForceFactor = 5f;

    [SerializeField] private float groundRayDistance = 1f;

    private Rigidbody _rigidbody;
    private Rigidbody[] _jumpBodies;
    private Vector3 _torque = Vector3.zero;
    private Vector3 _force = Vector3.zero;
    private bool _wantsJump = false;

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
        var ray = new Ray(transform.position, Vector3.down);
        bool onGround = Physics.Raycast(ray, out var hit, groundRayDistance);
        if (onGround && _wantsJump) {
            foreach (var jumpBody in _jumpBodies) {
                jumpBody.AddForce(Vector3.up * jumpFactor * jumpBody.mass, ForceMode.Impulse);
            }
        }

        if (onGround) {
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
}
