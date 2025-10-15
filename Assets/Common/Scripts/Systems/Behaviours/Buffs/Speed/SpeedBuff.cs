using UnityEngine;

public class SpeedBuff : Buff {
    private SpeedBuffData _speedData;
    public override BuffSO SO { get => _speedData; set => _speedData = value as SpeedBuffData; }
    public SpeedBuff(BuffSO buff, GameObject user) : base(buff, user) {
    }

    public override void OnAdded(GameObject user) {
        // user.Service<MovementService>().Add(_speedData.Boost);
    }

    public override void OnFinished(GameObject user) {
        // user.Service<MovementService>().Remove(_speedData.Boost);
    }

    public override void OnTick(GameObject user, float timer) {
    }
}