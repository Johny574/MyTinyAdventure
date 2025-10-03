


using DG.Tweening;
using FletcherLibraries;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class MainCamera : Singleton<MainCamera>
{
    public CameraController CameraController { get; set; }
    #region Shake
    [SerializeField] float _shakeAmount = 0.7f;
    [SerializeField] float _shakeDuration = 0f;
    [SerializeField] float _decreaseFactor = 1.0f;
    Vector3 _originalPos;
    #endregion

    #region PostProcessing
    Vignette _vignette;
    #endregion

    [SerializeField] CameraOptions _options;

    protected override void Awake() {
        base.Awake();
        CameraController = new CameraController(Camera.main, GameObject.FindGameObjectWithTag("Player"), _options);
        CameraController.Awake();
        var volume = GetComponent<Volume>();
        volume.profile.TryGet(out _vignette);
    }

    void Start() => CameraController.Start();

    void Update() {
        if (_shakeDuration > 0) {
            CameraController.Camera.transform.localPosition = _originalPos + UnityEngine.Random.insideUnitSphere * _shakeAmount;
            _shakeDuration -= Time.deltaTime * _decreaseFactor;
        }
        else {
            _shakeDuration = 0f;
            CameraController.Move();
            CameraController.Zoom(-Input.mouseScrollDelta.y);
        }
    }

    public void TriggerShake(float duration, float amount) {
        _originalPos = CameraController.Camera.transform.position;
        _shakeDuration = duration;
        _shakeAmount = amount;
    }

    public void FlashVignette(float durationIn = .2f, float durationOut = .4f) {
        float start = 0f;
        float stop = 0.5f;

        DOTween.Sequence()
        .Append(DOTween.To(() => start, x => {
            start = x;
            _vignette.intensity.value = x;
        }, stop, durationIn)).SetEase(Ease.OutElastic)
        .Append(DOTween.To(() => stop, x => {
            _vignette.intensity.value = x;
        }, start, durationOut)).SetEase(Ease.OutFlash);
    }

    void OnDrawGizmos() {
        if (!_options.DrawCameraBounds)
            return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_options.Bounds.center, _options.Bounds.size);
    }
}