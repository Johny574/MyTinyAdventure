using UnityEngine;

public class StaminaComponent : StatComponent, ISerializedComponent<BarData>
{
    StatpointsComponent _stats;
    public bool Sprinting = false;
    public bool CanSprint = true;

    public StaminaComponent(StaminaBehaviour behaviour) : base(behaviour)
    {
    }

    public void Initilize() {
        _stats = Behaviour.GetComponent<StatpointsBehaviour>().Stats;
        Data = new(_stats.StaminaPool, _stats.StaminaPool);
        Changed?.Invoke(Data);
    }

    public void Tick() {
        if (!CanSprint && Data.Amount > Data.Max * .1f)
            CanSprint = true;

        else if (Data.Amount <= 0)
            CanSprint = false;

        if (Data.Amount <= _stats.StaminaPool && !Sprinting)
                Update(_stats.StaminaRegen * Time.deltaTime);

            else
                Update(0 - Modifiers.SprintStaminaDrain);
    }

    public void Update(float amount) {
        Data.Amount+= amount;
        Data.Amount = Mathf.Clamp(Data.Amount, 0f, _stats.StaminaPool);
        Data.CalculateFill();
        Changed?.Invoke(Data);
    }

    public BarData Save() => Data;

    public void Load(BarData save) {
        Data = save;
        Changed?.Invoke(Data);
    }
}
