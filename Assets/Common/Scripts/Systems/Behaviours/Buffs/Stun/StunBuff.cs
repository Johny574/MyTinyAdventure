
using UnityEngine;

public class StunBuff : Buff {
    private StunBuffData _stunData;
    public override BuffSO SO { get => _stunData; set => _stunData = value as StunBuffData;}
    public StunBuff(BuffSO buff, GameObject user) : base(buff, user) {
    }
    
    public override void OnAdded(GameObject user) {
        // user.Service<MovementService>().Dynamic = false;
        // user.Service<MovementService>().Set(Vector2.zero);
        // user.Component<Rigidbody2D>().linearVelocity = Vector2.zero;
    }

    public override void OnFinished(GameObject user) {
        // user.Service<MovementService>().Dynamic = true;
    }

    public override void OnTick(GameObject user, float timer) {
    }
}