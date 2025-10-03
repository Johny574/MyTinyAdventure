using UnityEngine;

public class Projectile : MonoBehaviour, IPoolObject<ProjectileLaunchData>
{
    float _travelTime = 0f;
    Vector2 _direction = Vector2.zero;
    [SerializeField] private LayerMask _bounds;
    ProjectileLaunchData _launchData;


    // [SerializeField] private AudioSource _audio;
    // public bool HitDisable = true;
    // [SerializeField] public AudioClip LaunchAudio, HitAudio;
    // [SerializeField] private AnimatorOverrideController _hitEffect;
    // [SerializeField] private List<BuffSO> _debuffs;
    // GameObject _source;

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
        gameObject.layer = _launchData.Layer;
        Launch(variant);

        // if (_variant.GetComponent<Animator>().runtimeAnimatorController != null)
        // {
        //     GetComponent<Animator>().runtimeAnimatorController = _variant.GetComponent<Animator>().runtimeAnimatorController;
        //     GetComponent<Animator>().SetBool("Open", true);
        // }

        // LaunchAudio = _variant.LaunchAudio;
        // HitAudio = _variant.HitAudio;
        // HitDisable = _variant.HitDisable;
        // Lookoffset = _variant.Lookoffset;
    }

    public void Launch(ProjectileLaunchData variant) {
        // _audio.clip = LaunchAudio;
        // _audio.Play();
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

    // public float Damage() => _source.Service<StatService>().Stats["Attack"].Find(x => x.Data == Type.ToString()).Counter().Count;
    // public GameObject Source() => _source.Behaviour.gameObject;
    public void Hit(Collider2D collider) {
        // _audio.clip = HitAudio;
        // _audio.Play();

        // if (HitDisable) {
            // GetComponent<Animator>().SetBool("Open", false);
            // gameObject.SetActive(false);
        // }
    }

    // async void PlayHitEffect() {
    //     GameObject effect = await CentralManager.Instance.Manager<ObjectManager>().Pooler(Pooler.Type.Object, "FX").GetObject();
    //     effect.transform.position = transform.position;
    //     effect.GetComponent<Animator>().runtimeAnimatorController = _hitEffect;
    // }
    // public List<BuffSO> Buffs() => _debuffs;

}