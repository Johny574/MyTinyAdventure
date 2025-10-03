using UnityEngine;

[CreateAssetMenu(fileName = "_queststep", menuName = "Quests/TalktoNPCQueststep", order = 1)]
public class DialogueQueststepSO : QueststepData {
    // [field:SerializeField] public NPCEntityServiceBehaviour NPC { get; private set; }
    public string[] Dialogue;
    public GameObject Target;

    // public override Queststep Queststep(Quest quest, EntityService parttaker)
    // {
    //     return new TalkToNPCQueststep(ID, quest, parttaker);
    // }
}