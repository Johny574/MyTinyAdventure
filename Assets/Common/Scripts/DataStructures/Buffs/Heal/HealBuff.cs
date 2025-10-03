using UnityEngine;

public class HealBuff : Buff {
    private HealBuffData _healData;
    public override BuffSO SO { get => _healData; set => _healData = value as HealBuffData; }
    public HealBuff(BuffSO buff, GameObject user) : base(buff, user) {
    }

    public override void OnAdded(GameObject user) {
    }

    public override void OnFinished(GameObject user) {
    }

    public override void OnTick(GameObject user, float timer) {
        // new HealthCommands.AddCommand(_healData.Amount / _healData.Duration * timer, user.Service<HealthService>()).Execute();
    }
}