using UnityEngine;

public class GlobalLight2D : MonoBehaviour {
    public static GlobalLight2D Instance;
    [field: SerializeField] public Transform GlobalLightSource { get; private set; }
    [SerializeField] float _lightDistance = 50, _cycleDuration = 1440, _cycleTimer;
    [SerializeField] float _startAngle = 0, _stopAngle = 360, _currentAngle = 0;
    [SerializeField] Vector2 _pivot;

    void Awake() {
        Instance = this;
    }

    void Update() {
        if (_cycleTimer < _cycleDuration) {
            _cycleTimer += Time.deltaTime;
        }
        else {
            _cycleTimer = 0f;
            _currentAngle = 0f;
        }

        _currentAngle = Mathf.Lerp(_startAngle, _stopAngle, _cycleTimer / _cycleDuration);   
        GlobalLightSource.transform.position = _pivot + (Vector2)Rotation2D.GetPointOnCircle(_pivot, _currentAngle) * _lightDistance;
    }
}