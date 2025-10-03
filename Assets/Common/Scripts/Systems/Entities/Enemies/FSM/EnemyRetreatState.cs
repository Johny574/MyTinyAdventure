using UnityEngine;

public class EnemyRetreatState : StatemachineState<EnemyStateMachine, string>
{
    public EnemyRetreatState(EnemyStateMachine statemachine) : base(statemachine) {
    }

    public bool GetTransitionCondition() {
        return false;
    }

    public void OnAwake() {
    }

    public void Tick() {
        _statemachine.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
    }

    public void TransitionEnter() {
    }

    public void TransitionExit() {
    }
}