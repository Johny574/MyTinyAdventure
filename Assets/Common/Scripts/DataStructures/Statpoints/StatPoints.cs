using System;

[Serializable]
public struct StatPoints
{
      // todo : move this
    public enum Stat {
        Strength,
        Agility,
        Intelligence,
        Melee,
        Ranged,
        Magic
    }
    
    public StatPointsDictionary BaseStats;
    public StatPointsDictionary AttackStats;
    public StatPointsDictionary DefenseStats;

    public StatPoints(StatPointsDictionary baseStats, StatPointsDictionary attackStats, StatPointsDictionary defenseStats) {
        BaseStats = baseStats;
        AttackStats = attackStats;
        DefenseStats = defenseStats;
    }

    public void Add(StatPoints stats) {
        foreach (var key in stats.BaseStats.Keys)
            BaseStats[key] += stats.BaseStats[key];

        foreach (var key in stats.AttackStats.Keys)
            AttackStats[key] += stats.AttackStats[key];

        foreach (var key in stats.DefenseStats.Keys)
            DefenseStats[key] += stats.DefenseStats[key];
    }
    

    public void Remove(StatPoints stats) {
        foreach (var key in stats.BaseStats.Keys)
            BaseStats[key] -= stats.BaseStats[key];

        foreach (var key in stats.AttackStats.Keys)
            AttackStats[key] -= stats.AttackStats[key];

        foreach (var key in stats.DefenseStats.Keys)
            DefenseStats[key] -= stats.DefenseStats[key];
    }
}