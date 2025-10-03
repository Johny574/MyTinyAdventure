using System.Collections.Generic;
using UnityEngine;
public class ExperienceComponent : StatComponent, ISerializedComponent<BarData>
{
    private List<Level> _levels = new();
    public int Level { get; private set; } = 0;
    public int AvailableStatPoints { get; set; } = 0;
    FeedbackComponent _feedback;
    IPoolObject<string> _levelTag;
    
    public ExperienceComponent(ExperienceBehaviour behaviour, List<Level> levels) : base(behaviour) {
        _levels = levels;
        _feedback = new();
        Data = new(0, _levels[Level].XPRequirement);
        Changed?.Invoke(Data);
        Changed += async (data) => await _feedback.DisplayFeedback(new FeedbackData($"{data.Amount}", Color.blue), Behaviour.transform.position);
    }
    public void OnStart() => CreateLevelTag();

    async void CreateLevelTag() { 
        _levelTag = await LevelTagFactory.Instance.Pool.GetObject<string>();
        _levelTag.Bind((Level+1).ToString());
        ((MonoBehaviour)_levelTag).GetComponent<Follower>().Follow(Behaviour.gameObject);
    }
    public void Update(float amount) {
        Data.Amount += amount;
        Data.CalculateFill();
        Changed?.Invoke(Data);
        if (Data.Amount > _levels[Level].XPRequirement)
            LevelUp();
    }

    void LevelUp() {
        Level++;
        AvailableStatPoints += _levels[Level].StatPointsReward;
        _levelTag.Bind((Level+1).ToString());
    }
    public BarData Save() {
        return Data;
    }
    public void Load(BarData save) {
        Data = save;
    }
    public Level CurrentLevel => _levels[Level];

    // private void OnDeath() {
    //     if (_xp.Count <= 0) {
    //         return;
    //     }

    //     for (int i = 0; i < (_xp.Count / 50) - (_xp.Count % 50); i++) {
    //         new ExperienceCommands.DropCommand(50, Entity.Component<Transform>().position).Execute();
    //     }
    //     new ExperienceCommands.DropCommand(_xp.Count % 50, Entity.Component<Transform>().position).Execute();
    // }
}
