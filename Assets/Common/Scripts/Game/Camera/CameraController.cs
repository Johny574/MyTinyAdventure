using System;
using UnityEngine;

public class CameraController
{
    public Camera Camera { get; private set; }
    protected GameObject _player;
    float _zoom;
    GameObject _target;
    CameraOptions _options;

    public CameraController(Camera camera, GameObject player, CameraOptions options) {
        Camera = camera;
        _player = player;
        _options = options;
    }

    public void Awake() => Zoom(_options.ZoomMin);
    public void Start()
    {
        Focus(_player);
        Camera.transform.position = _player.transform.position;
    }

    public void Move() {
        Vector2 lerpPoint = Vector2.Lerp(Camera.transform.position, _target.transform.position, Time.deltaTime * _options.Damping);
        if (_options.Confined)
            lerpPoint = new Vector2(Mathf.Clamp(lerpPoint.x, _options.Bounds.min.x, _options.Bounds.max.x), Mathf.Clamp(lerpPoint.y, _options.Bounds.min.y, _options.Bounds.max.y));

        Camera.transform.position = new Vector3(lerpPoint.x, lerpPoint.y, -10f);
    }

    public void UnFocus() => Focus(_player);
    public void Focus(GameObject gameObject) => _target = gameObject;
    public void Zoom(float zoom) {
        _zoom += zoom;
        _zoom = Mathf.Clamp(_zoom, _options.ZoomMin, _options.ZoomMax);
        Camera.orthographicSize = _zoom;
    }
    
}


[Serializable]
public class CameraOptions {
    public float ZoomMin = 5f;
    public float ZoomMax = 10f;
    public float FocusZoom = 5f;
    public float Damping = 0f;
    public bool DrawCameraBounds = true, Confined = true;
    public Rect Bounds;
}