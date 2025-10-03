


using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(StatpointsBehaviour))]
[RequireComponent(typeof(StaminaBehaviour))]
public class MovementBehaviour : MonoBehaviour
{
    public MovementComponent Movement { get; set; }
    [SerializeField] ParticleSystem _moveParticles;
    protected virtual void Awake() {
        Movement = new(this, GetComponent<Rigidbody2D>(), _moveParticles);
    }

    protected void Start() {
        Movement.Initilize();
    }

    void Update() {
        Movement.Update();
    }
}