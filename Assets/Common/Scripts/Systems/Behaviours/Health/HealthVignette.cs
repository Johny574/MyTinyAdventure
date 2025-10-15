
using UnityEngine;


[RequireComponent(typeof(HealthBehaviour))]
public class HealthVignette : MonoBehaviour
{
    void Start() {
        HealthComponent health = GetComponent<HealthBehaviour>().Health;
        CameraController cameraController = Camera.main.GetComponent<MainCamera>().CameraController;
        health.Changed += (data, amount) =>
        {
            if (amount > 0) return;
            MainCamera.Instance.FlashVignette(Color.red);
        };
    }
}