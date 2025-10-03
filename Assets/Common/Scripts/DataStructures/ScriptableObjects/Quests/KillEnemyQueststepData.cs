using FletcherLibraries;
using UnityEngine;

[CreateAssetMenu(fileName = "_queststep", menuName = "Quests/KillEnemyQueststepData", order = 2)]

public class KillEnemyQueststepData : QueststepData {
    public SerializableDictionary<int, int> Target;
    // public override Queststep Queststep(Quest quest, EntityService parttaker)
    // {
    //     return new KillEnemiesQueststep(ID, quest, parttaker, new Stack<int>(Target.Data.ID, 0, Target.Counter().Limit));
    // }
}
