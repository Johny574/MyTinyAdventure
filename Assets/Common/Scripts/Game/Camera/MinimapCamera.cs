using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    CameraController _camera;
    [SerializeField] CameraOptions _cameraoptions;

    void Awake() {
        _camera = new CameraController(GetComponent<Camera>(), GameObject.FindGameObjectWithTag("Player"), _cameraoptions);
        _camera.Awake();
    }

    void Start() => _camera.Start();
    void Update() => _camera.Move();
}