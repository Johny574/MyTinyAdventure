using System;
using System.Collections.Generic;
using UnityEngine;

public class ParticleComponent : Component {
    private Dictionary<ParticleSystem, GameObject> _particles = new();
    public ParticleComponent(ParticleBehaviour behaviour) : base(behaviour) {
    }

    public void Add(ParticleSystem ps) {

    }

    public void Remove(ParticleSystem ps) {
        if (!_particles.ContainsKey(ps)) {
            return;
        }
        GameObject.Destroy(_particles[ps]);
        _particles.Remove(ps);
    }
}