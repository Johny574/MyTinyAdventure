
using UnityEngine;

[CreateAssetMenu(fileName = "_queststep", menuName = "Quests/ReachpointQueststep", order = 1)]
public class ReachpointQueststepData : QueststepSO {
     public Vector2 Position;
    // public override Queststep Queststep(Quest quest, EntityService parttaker)
    // {
    //     return new ReachpointQueststep(this.ID, quest, parttaker);
    // }
}