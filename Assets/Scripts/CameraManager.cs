using GodComplex.Utility;
using UnityEngine;

public class CameraManager : Singleton<CameraManager> {
    [SerializeField] private GameObject normalCamera = default;
    [SerializeField] private GameObject winCamera = default;

    public void SetNormalCamera() {
        normalCamera.SetActive(true);
        winCamera.SetActive(false);
    }

    public void SetWinCamera() {
        normalCamera.SetActive(false);
        winCamera.SetActive(true);
    }
}
