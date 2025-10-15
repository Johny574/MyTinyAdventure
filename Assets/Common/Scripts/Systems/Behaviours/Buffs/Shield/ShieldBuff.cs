using UnityEngine;

public class ShieldBuff : Buff {
    private ShieldBuffData _shieldData;
    public override BuffSO SO { get => _shieldData; set => _shieldData = value as ShieldBuffData; }

    public ShieldBuff(BuffSO buff, GameObject user) : base(buff, user) {
    }

    public override void OnAdded(GameObject user) {
        // user.Service<MovementService>().Add(_shieldData._boost);
    }

    public override void OnFinished(GameObject user) {
        // user.Service<MovementService>().Remove(_shieldData._boost);
    }

    public override void OnTick(GameObject user, float timer) {
    }
}