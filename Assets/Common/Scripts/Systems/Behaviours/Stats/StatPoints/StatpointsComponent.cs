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
        _xp.Level + 1 * Modifiers.HPPerLevel +
        StatPoints.BaseStats[StatPoints.Stat.Strength] * Modifiers.HPPerStatpoint;
    }

    public float HPRegen {
        get =>
        _xp.Level + 1 * Modifiers.HPRegenPerLevel +
        StatPoints.BaseStats[StatPoints.Stat.Strength] * Modifiers.HPRegenPerStatpoint;
    }

    public int StaminaPool {
        get =>
        _xp.Level + 1 * Modifiers.StaminaPerLevel +
        StatPoints.BaseStats[StatPoints.Stat.Agility] * Modifiers.StaminaPerStatpoint;
    }

    public float StaminaRegen {
        get =>
        _xp.Level + 1 * Modifiers.StaminaRegenPerLevel +
        StatPoints.BaseStats[StatPoints.Stat.Agility] * Modifiers.StaminaRegenPerStatpoint;
    }

    public int ManaPool {
        get =>
        _xp.Level + 1 * Modifiers.ManaPerLevel +
        StatPoints.BaseStats[StatPoints.Stat.Intelligence] * Modifiers.ManaPerStatpoint;
    }
    
    public float ManaRegen {
        get =>
        _xp.Level + 1 * Modifiers.ManaRegenPerLevel +
        StatPoints.BaseStats[StatPoints.Stat.Intelligence] * Modifiers.ManaRegenPerStatpoint;
    }

    public float MoveSpeed {
        get =>
        Modifiers.DefaultMovespeed + 
        _xp.Level + 1 * Modifiers.MovespeedPerLevel +
        StatPoints.BaseStats[StatPoints.Stat.Agility] * Modifiers.MoveSpeedPerStatpoint;
    }

    public float SprintSpeed {
        get =>
        Modifiers.DefaultSprintSpeed + 
        _xp.Level + 1 * Modifiers.SprintSpeedPerLevel +
        StatPoints.BaseStats[StatPoints.Stat.Agility] * Modifiers.SprintSpeedPerStatpoint;
    }
}