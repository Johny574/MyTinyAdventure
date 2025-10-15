using System.Collections.Generic;
using UnityEngine;
public class ExperienceComponent : StatComponent, ISerializedComponent<BarData>
{
    private List<Level> _levels = new();
    public int Level { get; private set; } = 0;
    public int AvailableStatPoints { get; set; } = 0;
    IPoolObject<string> _levelTag;
    
    public ExperienceComponent(ExperienceBehaviour behaviour, List<Level> levels, float xp) : base(behaviour) {
        _levels = levels;
        Data = new(xp, _levels[Level].XPRequirement);
        Changed?.Invoke(Data, 0);
        Changed += async (bardata, amount) =>
        {
            if (Mathf.Sign(amount) == 0)
                return;
            await FeedbackFactory.Instance.Display(new($"{amount}", Color.blue), Behaviour.transform.position);
        };
    }

    public void Initilize(HealthComponent health)
    {
        health.Death += OnDeath;
        CreateLevelTag();
    }

    async void CreateLevelTag() {
        _levelTag = await LevelTagFactory.Instance.Pool.GetObject<string>();
        _levelTag.Bind((Level + 1).ToString());
        ((MonoBehaviour)_levelTag).GetComponent<Follower>().Follow(Behaviour.gameObject);
    }

    public void Update(float amount) {
        Data.Amount += amount;
        Data.CalculateFill();
        Changed?.Invoke(Data, amount);
        if (Data.Amount > _levels[Level].XPRequirement)
            LevelUp();
    }

    void LevelUp() {
        Level++;
        AvailableStatPoints += _levels[Level].StatPointsReward;
        _levelTag.Bind((Level+1).ToString());
    }

    public BarData Save() => Data;
    public void Load(BarData save) => Data = save;
    public Level CurrentLevel => _levels[Level];

    void OnDeath() {
        DropCurrency();
        ((MonoBehaviour)_levelTag).gameObject.SetActive(false);
    }

    void DropCurrency() {
        if (Data.Amount <= 0)
            return;

        var n = Data.Amount - (Data.Amount % Modifiers.MaxXPOrbSize);
        for (int i = 0; i < (n/ Modifiers.MaxXPOrbSize); i++)
            new ExperienceCommands.DropCommand(Modifiers.MaxXPOrbSize, Behaviour.transform.position).Execute();

        new ExperienceCommands.DropCommand(Data.Amount % Modifiers.MaxXPOrbSize, Behaviour.transform.position).Execute();
    }
}
