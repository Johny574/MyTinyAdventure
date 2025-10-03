



using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemyRecoverState : StatemachineState<MeleeEnemyStatemachine, string>, IStatemachineState
{
    NavMeshAgent _agent;
    Vector3 retreatPoint;
    NavMeshPath _path;
    GameObject _player;
    PatrolComponent _patrol;
    GearComponent _gear;
    MovementComponent _movement;
    public MeleeEnemyRecoverState(MeleeEnemyStatemachine statemachine, NavMeshAgent agent, PatrolComponent patrol, GearComponent gear, MovementComponent movement, GameObject player) : base(statemachine) {
        _agent = agent;
        _player = player;
        _patrol = patrol;
        _movement = movement;
        _gear = gear;
        
    }

    public bool GetTransitionCondition() {
        return _statemachine.CanAttack == false;
    }

    public void OnAwake() {

    }

    public void Tick() {
        _statemachine.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        if (_agent.path == null || Vector2.Distance(_player.transform.position, retreatPoint) < ((MeleeWeaponItemData)_gear.Gear[GearItemSO.Slot.Primary].Item).Reach) {
            if (RandomPoint(_player.transform.position, 10f, out retreatPoint)) {
                _path = new();
                // _agent.CalculatePath((Vector3)retreatPoint, _path);
                // _agent.SetPath(_path);
                // Debug.DrawLine(_statemachine.transform.position, retreatPoint, Color.red);
                // Debug.Break();
            }
        }
        // else
        // {
        //     _movement.FrameInput = _agent.desiredVelocity.normalized;
        // }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result) {
        for (int i = 0; i < 30; i++) {
            Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    public void TransitionEnter() {
        // _agent.path = null;
        retreatPoint = _statemachine.transform.position;
        _path = null;
    }

    public void TransitionExit() {
    }
}