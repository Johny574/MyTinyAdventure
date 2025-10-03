using System;
public class StatpointsComponent : Component, ISerializedComponent<StatPoints>
{
    public StatPoints StatPoints { get; set; }
    ExperienceComponent _xp;
    GearComponent _gear;
    public Action<StatPoints> Changed;

    public StatpointsComponent(StatpointsBehaviour behaviour, StatPoints statpoints, ExperienceComponent xp) : base(behaviour) {
        StatPoints = statpoints;
        _xp = xp;
    }

    public void Initilize(GearComponent gear) {
        _gear = gear;
        _gear.Equiped += Add;
        _gear.Unequiped += Remove;
    }

    private void Add(GearItemSO sO) {
        StatPoints.Add(sO.Stats);
        Changed?.Invoke(StatPoints);
    }

    private void Remove(GearItemSO sO) {
        StatPoints.Remove(sO.Stats);
        Changed?.Invoke(StatPoints);
    }

    public StatPoints Save() => StatPoints;
    public void Load(StatPoints save) => StatPoints = save;

    public int HPPool {
        get =>
        _xp.Level + 1 * StatpointsModifiers.HPPerLevel +
        StatPoints.BaseStats[StatPoints.Stat.Strength] * StatpointsModifiers.HPPerStatpoint;
    }

    public float HPRegen {
        get =>
        _xp.Level + 1 * StatpointsModifiers.HPRegenPerLevel +
        StatPoints.BaseStats[StatPoints.Stat.Strength] * StatpointsModifiers.HPRegenPerStatpoint;
    }

    public int StaminaPool {
        get =>
        _xp.Level + 1 * StatpointsModifiers.StaminaPerLevel +
        StatPoints.BaseStats[StatPoints.Stat.Agility] * StatpointsModifiers.StaminaPerStatpoint;
    }

    public float StaminaRegen {
        get =>
        _xp.Level + 1 * StatpointsModifiers.StaminaRegenPerLevel +
        StatPoints.BaseStats[StatPoints.Stat.Agility] * StatpointsModifiers.StaminaRegenPerStatpoint;
    }

    public int ManaPool {
        get =>
        _xp.Level + 1 * StatpointsModifiers.ManaPerLevel +
        StatPoints.BaseStats[StatPoints.Stat.Intelligence] * StatpointsModifiers.ManaPerStatpoint;
    }
    
    public float ManaRegen {
        get =>
        _xp.Level + 1 * StatpointsModifiers.ManaRegenPerLevel +
        StatPoints.BaseStats[StatPoints.Stat.Intelligence] * StatpointsModifiers.ManaRegenPerStatpoint;
    }

    public float MoveSpeed {
        get =>
        StatpointsModifiers.DefaultMovespeed + 
        _xp.Level + 1 * StatpointsModifiers.MovespeedPerLevel +
        StatPoints.BaseStats[StatPoints.Stat.Agility] * StatpointsModifiers.MoveSpeedPerStatpoint;
    }

    public float SprintSpeed {
        get =>
        StatpointsModifiers.DefaultSprintSpeed + 
        _xp.Level + 1 * StatpointsModifiers.SprintSpeedPerLevel +
        StatPoints.BaseStats[StatPoints.Stat.Agility] * StatpointsModifiers.SprintSpeedPerStatpoint;
    }
}