using UnityEngine;

public class DamageBuff : Buff {
    private DamageBuffData _damageData;
    public override BuffSO SO { get => _damageData; set => _damageData = value as DamageBuffData; }
    public DamageBuff(BuffSO buff, GameObject user) : base(buff, user) {

    }

    public override void OnAdded(GameObject user) {
    }

    public override void OnFinished(GameObject user) {
    }

    public override void OnTick(GameObject user, float timer) {
        // user.Service<HealthService>().Remove(_damageData.DPS);  
    }
}