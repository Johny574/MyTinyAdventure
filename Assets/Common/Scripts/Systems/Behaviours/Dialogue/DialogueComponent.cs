using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogueComponent : Component
{
    public Dialogue ActiveDialogue { get; set; }
    Dialogue _defaultActiveDialogue = new(null, new DialogueExchange[0]);
    JournalComponent _journal;
    EmoteComponent _emote;
    Sprite _questEmote, _dialogueEmote;
    Entity _entity;
    public DialogueComponent(DialogueBehaviour behaviour, Dialogue action, EmoteComponent emote, Sprite questEmote, Sprite dialogueEmote) : base(behaviour)
    {
        _defaultActiveDialogue = action;
        _emote = emote;
        _questEmote = questEmote;
        _dialogueEmote = dialogueEmote;
    }

    public void Initilize(JournalComponent journal) {
        _journal = journal;
        CreateTag();

        if (_journal.Quests.Count > 0)
            _emote.Add(_questEmote);
    }

    public void Interact(GameObject accesor) {
        MainCamera.Instance?.CameraController.Focus(Behaviour.gameObject);
        ActiveDialogue = GetActiveDialogue(accesor);
        DialogueController.Instance.Open(ActiveDialogue);
        _emote.Add(_dialogueEmote);
    }

    // loop over entitys talking then loop over each entitys dialogue...
    public void CancelTarget() {
        MainCamera.Instance.CameraController.UnFocus();
        DialogueController.Instance.Close(false);
        ShopEvents.Instance.Close.Invoke();

        _emote.Remove();

        if (_journal.Quests.Count > 0)
            _emote.Add(_questEmote);

    }

    public void Target() { }

    async void CreateTag() {
        IPoolObject<string> tag = await TagFactory.Instance.Pool.GetObject<string>();
        ((MonoBehaviour)tag).GetComponent<Follower>().Follow(Behaviour.gameObject);
        tag.Bind(Behaviour.gameObject.name);
    }

    /// <summary>
    /// Gets the active dialogue according to the current situation.
    /// </summary>
    /// <param name="accesor">The accesor's journal</param>
    /// <returns></returns>
    Dialogue GetActiveDialogue(GameObject accesor) {
        QuestingComponent questing = accesor.GetComponent<QuestingBehaviour>().Questing;
        Dialogue? activeDialogue = GetActiveDialogueForDialogueQueststeps(questing);

        if (activeDialogue == null)
            activeDialogue = GetActiveDialogueForQuests(questing);

        if (activeDialogue == null)
            activeDialogue = GetActiveDialogueForShop(accesor);

        if (activeDialogue == null)
            activeDialogue = _defaultActiveDialogue;

        return (Dialogue)activeDialogue;
    }

    /// <summary>
    /// Checks if current npc sells item and handles dialogue accordingly.
    /// </summary>
    /// <returns></returns>
    Dialogue? GetActiveDialogueForShop(GameObject accesor) {
        ShopBehaviour shop;
        if (Behaviour.TryGetComponent(out shop))
            return shop.Shop.Dialogue(accesor);
        else
            return null;
    }

    /// <summary>
    /// Checks if the npc has a available quest and handles dialogue accordingly.
    /// </summary>
    /// <param name="journal">The accesor's journal component.</param>
    /// <returns></returns>
    Dialogue? GetActiveDialogueForQuests(JournalComponent accesor) {
        if (_journal.Quests.Count > 0) {
            QuestSO firstAvailable = Behaviour.GetComponent<QuestingBehaviour>().Questing.ActiveQuests[0].SO;
            return new Dialogue(() => accesor.Quests.Add(firstAvailable), new DialogueExchange[1] { new DialogueExchange(firstAvailable.StartDialogue, _entity) });
        }

        return null;
    }

    /// <summary>
    /// Checks if the player has any quest where he currently needs to talk to this npc and handles dialogue accordingly.
    /// </summary>
    /// <param name="accesor">The accessor's questing component.</param>
    /// <returns></returns>
    Dialogue? GetActiveDialogueForDialogueQueststeps(QuestingComponent accesor) {
        List<Quest> accesorQuests = accesor.ActiveQuests;

        if (accesorQuests.Count <= 0)
            return null;

        List<Quest> activeDialogueQuests = accesorQuests
        .Where(x => x.CurrentStep().GetType().Equals(typeof(DialogueQueststep)))?
        .Where(x => (x.CurrentStep().SO as DialogueQueststepSO).Target.GetComponent<Entity>().UID.Equals(Behaviour.GetComponent<Entity>().UID)).ToList();

        if (activeDialogueQuests.Count <= 0)
            return null;


        //  (DialogueQueststepSO)activeDialogueQuests[0].CurrentStep().Data).Dialogue
        return new Dialogue(() => activeDialogueQuests[0].CurrentStep().Complete(), ((DialogueQueststepSO)activeDialogueQuests[0].CurrentStep().SO).Dialogue);
    }
}