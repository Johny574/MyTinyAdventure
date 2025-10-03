// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.AI;

// public class AnimalPatrolstate : StatemachineState<string> {
//      private GameObject _entity;
//     private NavMeshPath _path = new();
//     private NavMeshAgent _agent;
//     private PatrolComponent _patrolService;

//     // public AnimalPatrolstate(EntityService entity) {
//     //     _entity = entity;
//     //     _agent = _entity.Component<NavMeshAgent>();
//     //     _patrolService = _entity.Service<PatrolService>();
//     // }

//     public override bool GetTransitionCondition() {
//         // KeyValuePair<int, List<PatrolPoint>> _patrolData = _patrolService.Get<KeyValuePair<int, List<PatrolPoint>>>();
//         // return Vector2.Distance(_patrolData.Value[_patrolData.Key].Position, _agent.transform.position) > 1f;
//         return false;
//     }

//     public override void OnAwake() {
//         // _patrolService.Remove(0);
//     }

//     public override void Tick() {
//     //     _agent.transform.rotation = Quaternion.Euler(new Vector3(0f,0f,0f));
//     //     KeyValuePair<int, List<PatrolPoint>> _patrolData = _patrolService.Get<KeyValuePair<int, List<PatrolPoint>>>();
//     //     _agent.CalculatePath(_patrolData.Value[_patrolData.Key].Position, _path);
//     //     _agent.SetPath(_path);
//     //     _entity.Component<Rigidbody2D>().linearVelocity = _agent.desiredVelocity.normalized * 2f;    
//     }

//     public override void TransitionEnter() {
//         // _entity.Component<Animator>().CrossFade("Run", 0f);
//         // _entity.Service<AudioService>().Get<List<AudioSetting<string>>>().Find(x => x.Key == "Walk").Source.Play();
//     }

//     public override void TransitionExit() {
//         // _entity.Service<AudioService>().Get<List<AudioSetting<string>>>().Find(x => x.Key == "Walk").Source.Stop();
//     }
// }
