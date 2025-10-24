using System;
using UnityEngine;

public class MovementComponent : Component {
    ParticleSystem _moveParticles;
    Rigidbody2D _rb;
    public Vector2 FrameInput  = Vector2.zero;
    public float CurrentSpeed;
    public bool CanMove = true;

    public MovementComponent(MovementBehaviour behaviour, Rigidbody2D rb, ParticleSystem moveParticles) : base(behaviour) {
        _moveParticles = moveParticles;
        _rb = rb;
    }

    public StatpointsComponent Stats { get; set; }
    public StaminaComponent Stamina { get; set; }
    
    public void Initilize() {
        Stats = Behaviour.GetComponent<StatpointsBehaviour>().Stats;
        Stamina = Behaviour.GetComponent<StaminaBehaviour>().Stamina;
    }

    public void Update() {
        if (!CanMove)
            return;
        
        CurrentSpeed = Stamina.Sprinting ? Stats.SprintSpeed : Stats.MoveSpeed;

        _rb.linearVelocity = FrameInput * CurrentSpeed;
        // _behaviour.transform.position = new Vector3(_behaviour.transform.position.x, _behaviour.transform.position.y, 0f);

        HandleParticles();
    }

    void HandleParticles() { 
        if (FrameInput == Vector2.zero) {
            _moveParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            return;
        }
        
        if (_moveParticles.isStopped) 
            _moveParticles.Play();

        Vector2 dif =  FrameInput.normalized;
        dif = dif * -1;
        ParticleSystem.VelocityOverLifetimeModule forceOverLifetimeModule = _moveParticles.velocityOverLifetime;
        
        AnimationCurve curve = new AnimationCurve();

        curve.AddKey(0.0f,  dif.x);
        curve.AddKey(0.75f,  dif.x);
        
        forceOverLifetimeModule.x = new ParticleSystem.MinMaxCurve(1.5f, curve);
        
        curve = new AnimationCurve();
        
        curve.AddKey(0.0f,  dif.y);
        curve.AddKey(0.75f,   dif.y);
        
        forceOverLifetimeModule.y = new ParticleSystem.MinMaxCurve(1.5f, curve);
    }

}