using System.Collections.Generic;

public class PlayerSaveData
{
    public BarData Health;
    public BarData Stamina;
    public BarData Mana;
    public BarData Experience;
    public StatPoints Stats;
    public ItemStackData[] Inventory;
    public Currency Currency;
    public List<BuffData> Buffs;
    public SkillData[] Skills;
    public GearSlotData[] Gear;
    public QuestData[] Journal;
    public ConsumableData[] Consumables;
    public float X, Y;
    public string CurrentScene;

    public PlayerSaveData(BarData health, BarData mana, BarData stamina, BarData experience, StatPoints stats, ItemStackData[] inventory, Currency currency, List<BuffData> buffs, SkillData[] skills, GearSlotData[] gear, QuestData[] journal, ConsumableData[] consumables, float x, float y, string currentScene) {
        Health = health;
        Mana = mana;
        Stamina = stamina;
        Experience = experience;
        Inventory = inventory;
        Currency = currency;
        Buffs = buffs;
        Stats = stats;
        Skills = skills;
        Gear = gear;
        Journal = journal;
        Consumables = consumables;
        X = x;
        Y = y;
        CurrentScene = currentScene;
    }
}