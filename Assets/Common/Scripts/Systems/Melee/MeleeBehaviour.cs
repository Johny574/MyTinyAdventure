using UnityEngine;

public class MeleeBehaviour : MonoBehaviour
{
    public MeleeComponent Melee { get; set; }

    [Range(0, 180)]
    [SerializeField] float _meleeSwingArcDegrees = 135;
    [SerializeField] TrailRenderer _trailRenderer;
    [SerializeField] LayerMask _enemies;
    [SerializeField] AudioSource _whooshAudio;

    void Awake() {
        Melee = new MeleeComponent(this, _trailRenderer, _meleeSwingArcDegrees, _enemies, _whooshAudio);
    }

    
    void Start() {
        Melee.Initilize(GetComponent<GearBehaviour>().Gear, GetComponent<HandsBehaviour>().Hands, GetComponent<FlipBehaviour>().Flip, GetComponent<StatpointsBehaviour>().Stats);
    }
}