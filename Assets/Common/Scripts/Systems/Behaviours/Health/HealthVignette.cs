
using UnityEngine;


[RequireComponent(typeof(HealthBehaviour))]
public class HealthVignette : MonoBehaviour
{
    void Start() {
        HealthComponent health = GetComponent<HealthBehaviour>().Health;
        CameraController cameraController = Camera.main.GetComponent<MainCamera>().CameraController;
        health.Changed += (data) => MainCamera.Instance.FlashVignette();
    }
}