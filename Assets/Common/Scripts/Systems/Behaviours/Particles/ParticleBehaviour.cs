


using UnityEngine;

public class ParticleBehaviour : MonoBehaviour {
    
    public ParticleComponent Particles;
    void Awake() {
        Particles = new(this);
    } 
}