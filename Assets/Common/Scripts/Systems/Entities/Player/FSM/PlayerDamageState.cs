using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageState : StatemachineState<PlayerStateMachine, string>, IStatemachineState{
    CameraController _cameraController;

    public PlayerDamageState(PlayerStateMachine statemachine, CameraController cameraController) : base(statemachine) {
        _cameraController = cameraController;
    }
    public bool GetTransitionCondition() {
        // return _entity.Service.Service<CollisionService>().Get<List<IDamage>>().Count > 0;        
        return false;
    }

    public void OnAwake() {

    }

    public void Tick() {
  
    }

    public void TransitionEnter() {
        // IDamage source = _entity.Service.Service<CollisionService>().Get<List<IDamage>>()[0];

        // new HealthCommands.RemoveCommand(_entity.Service, source.Source().GetComponent<EntityServiceBehaviour>().Service, source.Damage()).Execute();

        // if (_entity.Service.Service<HealthService>().Dead) {
        //     return;
        // }

        // source.Hit(_entity.Service.Component<Collider2D>());

        // _entity.Service.Service<AudioService>().AudioSettings["Damage"].Source.Stop();
        // _entity.Service.Service<AudioService>().AudioSettings["Damage"].PlayRandom();

        // _entity.Service.Service<AudioService>().AudioSettings["Impact"].Source.Stop();
        // _entity.Service.Service<AudioService>().AudioSettings["Impact"].PlayRandom();

        // if (!_entity.Service.Component<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Death")) {
        //     _entity.Service.Component<Animator>().Play("Hit");
        // }

        // if (!_entity.Service.Service<KnockbackService>().CollidesWithWall((Vector2)source.Source().GetComponent<EntityServiceBehaviour>().Service.Component<Transform>().position)) {
        //     _entity.Service.Service<KnockbackService>().Add((Vector2)source.Source().GetComponent<EntityServiceBehaviour>().Service.Component<Transform>().position);
        // }
        // else {
        //     source.Source().GetComponent<EntityServiceBehaviour>().Service.Service<KnockbackService>().Add((Vector2)_entity.Service.Component<Transform>().position);
        // }

        // if (source is IBuffSource) {
        //     foreach (var buff in (source as IBuffSource).Buffs()) {
        //         _entity.Service.Service<BuffService>().Add(buff);
        //     }
        // }
        
        // _cameraController.Vignette.intensity.value = .25f;
    }

    public void TransitionExit() {
        // _cameraController.StartCoroutine(_cameraController.PlayCameraNoise());
        // _cameraController.StartCoroutine(_cameraController.PlayVignette(Color.red));
    }
}