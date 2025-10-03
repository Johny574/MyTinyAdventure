using UnityEngine;

public class Player : Singleton<Player> {
   public HealthBehaviour Health;
    public StaminaBehaviour Stamina;
    public ManaBehaviour Mana;
    public ExperienceBehaviour Experience;
    public InventoryBehaviour Inventory;
    public InteractBehaviour Interact;
    public CurrencyBehaviour Currency;
    public BuffsBehaviour Buffs;
    public SkillsBehaviour Skills;
    public ConsumableBehaviour Consumables;
    public StatpointsBehaviour Stats;
    public GearBehaviour Gear;
    public QuestingBehaviour Journal;
    public LocationBehaviour Location;
    public CacheBehaviour Cache;
    public SpriteRenderer Renderer;
    protected override void Awake() {
        base.Awake();
        Health = GetComponent<HealthBehaviour>();
        Cache = GetComponent<CacheBehaviour>();
        Stamina = GetComponent<StaminaBehaviour>();
        Mana = GetComponent<ManaBehaviour>();
        Inventory = GetComponent<InventoryBehaviour>();
        Interact = GetComponent<InteractBehaviour>();
        Experience = GetComponent<ExperienceBehaviour>();
        Currency = GetComponent<CurrencyBehaviour>();
        Buffs = GetComponent<BuffsBehaviour>();
        Skills = GetComponent<SkillsBehaviour>();
        Consumables = GetComponent<ConsumableBehaviour>();
        Stats = GetComponent<StatpointsBehaviour>();
        Gear = GetComponent<GearBehaviour>();
        Journal = GetComponent<QuestingBehaviour>();
        Location = GetComponent<LocationBehaviour>();
        Renderer = GetComponent<SpriteRenderer>();
    }
}