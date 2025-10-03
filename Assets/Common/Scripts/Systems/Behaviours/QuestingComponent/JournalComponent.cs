using System.Collections.Generic;

public class JournalComponent :  Component {
    public JournalComponent(UnityEngine.MonoBehaviour behaviour, List<QuestSO> quests) : base(behaviour) {
        Quests = quests;
    }

    public List<QuestSO> Quests { get; set; } = new();
}