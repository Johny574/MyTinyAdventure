using UnityEngine;

public class Projectile : MonoBehaviour, IPoolObject<ProjectileLaunchData>
{
    float _travelTime = 0f;
    Vector2 _direction = Vector2.zero;
    [SerializeField] private LayerMask _bounds;
    ProjectileLaunchData _launchData;

    Animator _animator;
    SpriteRenderer _renderer;
    TrailRenderer _trialRenderer;
    void Start() {
        _renderer = GetComponent<SpriteRenderer>();
        _trialRenderer = GetComponent<TrailRenderer>();
    }

    public void Bind(ProjectileLaunchData variant) {
        if (_renderer == null)
            _renderer = GetComponent<SpriteRenderer>();

        if (_animator == null)
            _animator = GetComponent<Animator>();

        if (_trialRenderer == null)
            _trialRenderer = GetComponent<TrailRenderer>();

        _launchData = variant;
        _renderer.sprite = _launchData.Variant.Sprite;
    }

    public void Launch(ProjectileLaunchData variant) {
        // _audio.clip = LaunchAudio;
        // _audio.Play();
        Bind(variant);
        _direction = variant.Direction;
        transform.position = variant.Position;
        _trialRenderer.Clear();
        gameObject.SetActive(true);
    }

    void Update() {
        if (_travelTime < _launchData.Variant.TravelDuration) {
            _travelTime += Time.deltaTime;
        }
        else {
            gameObject.SetActive(false);
            _direction = Vector2.zero;
            _travelTime = 0f;
            _trialRenderer.Clear();
        }

        transform.rotation = Quaternion.Euler(0, 0, Rotation2D.LookAngle(_direction, _launchData.Variant.Lookoffset));
        transform.position = (Vector2)transform.position + _direction * _launchData.Variant.TravelSpeed * Time.deltaTime;
    }


    public void OnTriggerEnter2D(Collider2D col) {
        if (!col.gameObject.layer.Equals(_launchData.Target))
            return;

        col.GetComponent<EntityStatemachine>().TakeDamage(transform.position, _launchData.Stats);
        gameObject.SetActive(false);
    }

    public void Hit(Collider2D collider)
    {
        // _audio.clip = HitAudio;
        // _audio.Play();

        // if (HitDisable) {
        // GetComponent<Animator>().SetBool("Open", false);
        // gameObject.SetActive(false);
        // }
    }


}