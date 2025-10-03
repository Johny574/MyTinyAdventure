


using UnityEngine;

[RequireComponent(typeof(StatpointsBehaviour))]
public class HealthBehaviour : MonoBehaviour
{
    public HealthComponent Health { get; set; }
    [SerializeField] ParticleSystem _bloodparticles;

    void Awake() {
        Health = new(this, _bloodparticles);
    }

    void Start() {
        Health.Initilize();
    }

    public void Die() => Health.Die();
}