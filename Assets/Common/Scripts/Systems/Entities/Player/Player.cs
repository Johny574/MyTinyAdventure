using UnityEngine;

public class Player : Singleton<Player> {
   public HealthBehaviour Health { get; private set; }
    public StaminaBehaviour Stamina { get; private set; }
    public ManaBehaviour Mana { get; private set; }
    public ExperienceBehaviour Experience { get; private set; }
    public InventoryBehaviour Inventory { get; private set; }
    public InteractBehaviour Interact { get; private set; }
    public CurrencyBehaviour Currency { get; private set; }
    public BuffsBehaviour Buffs { get; private set; }
    public SkillsBehaviour Skills { get; private set; }
    public ConsumableBehaviour Consumables { get; private set; }
    public StatpointsBehaviour Stats { get; private set; }
    public GearBehaviour Gear { get; private set; }
    public QuestingBehaviour Journal { get; private set; }
    public LocationBehaviour Location { get; private set; }
    public CacheBehaviour Cache { get; private set; }
    public SpriteRenderer Renderer { get; private set; }
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