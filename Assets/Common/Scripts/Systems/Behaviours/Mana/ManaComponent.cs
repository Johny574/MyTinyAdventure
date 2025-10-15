using System;
using UnityEngine;
public class ManaComponent : StatComponent, ISerializedComponent<BarData>
{
    StatpointsComponent _stats;

    public ManaComponent(ManaBehaviour behaviour) : base(behaviour) {
    }

    public void Initilize() {
        _stats = Behaviour.GetComponent<StatpointsBehaviour>().Stats;
        Data = new (_stats.ManaPool, _stats.ManaPool);
        Changed?.Invoke(Data, 0);
    }

    public void Tick() {
        if (Data.Amount < _stats.ManaPool) {
            Update(_stats.ManaRegen * Time.deltaTime);
        }
    }

    public void Update(float amount) {
        Data.Amount += amount;
        Data.Amount = Mathf.Clamp(Data.Amount, 0, _stats.ManaPool);
        Data.CalculateFill();
        Changed?.Invoke(Data, 0);
    }

    // todo : change invoke
    // todo : what if the pool gets bigger u need to set the _data.max to otherwise bars will update with old value
    public BarData Save() {
        return Data;
    }

    public void Load(BarData save) {
        Data = save;
        Changed?.Invoke(Data, 0);
    }
}