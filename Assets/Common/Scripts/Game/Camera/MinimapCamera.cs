using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    CameraController _camera;
    [SerializeField] CameraOptions _options;

    void Awake() {
        _camera = new CameraController(GetComponent<Camera>(), GameObject.FindGameObjectWithTag("Player"), _options);
        _camera.Awake();
    }

    void Start() => _camera.Start();
    void Update() => _camera.Move();

     public void OnDrawGizmos()
    {
        if (!_options.DrawCameraBounds)
            return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_options.Bounds.center, _options.Bounds.size);
    }
}