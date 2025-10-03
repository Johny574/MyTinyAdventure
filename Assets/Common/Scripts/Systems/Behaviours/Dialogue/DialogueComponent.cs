using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogueComponent : Component
{
    public ActiveDialogue ActiveDialogue { get; set; }
    ActiveDialogue _defaultActiveDialogue = new(new string[0], null);
    JournalComponent _journal;
    EmoteComponent _emote;
    Sprite _questEmote, _dialogueEmote;

    public DialogueComponent(DialogueBehaviour behaviour, ActiveDialogue action, EmoteComponent emote, Sprite questEmote, Sprite dialogueEmote) : base(behaviour) {
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
        DialogueController.Instance.Open(ActiveDialogue, Behaviour.gameObject);
        _emote.Add(_dialogueEmote);
    }

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
    ActiveDialogue GetActiveDialogue(GameObject accesor) {
        QuestingComponent questing = accesor.GetComponent<QuestingBehaviour>().Questing;
        ActiveDialogue? activeDialogue = GetActiveDialogueForDialogueQueststeps(questing);

        if (activeDialogue == null)
            activeDialogue = GetActiveDialogueForQuests(questing);

        if (activeDialogue == null)
            activeDialogue = GetActiveDialogueForShop(accesor);

        if (activeDialogue == null)
            activeDialogue = _defaultActiveDialogue;

        return (ActiveDialogue)activeDialogue;
    }

    /// <summary>
    /// Checks if current npc sells item and handles dialogue accordingly.
    /// </summary>
    /// <returns></returns>
    ActiveDialogue? GetActiveDialogueForShop(GameObject accesor) {
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
    ActiveDialogue? GetActiveDialogueForQuests(JournalComponent accesor) {
        if (_journal.Quests.Count > 0) {
            QuestSO firstAvailable = Behaviour.GetComponent<QuestingBehaviour>().Questing.ActiveQuests[0].SO;
            return new ActiveDialogue(firstAvailable.StartDialogue, () => accesor.Quests.Add(firstAvailable));
        }

        return null;
    }

    /// <summary>
    /// Checks if the player has any quest where he currently needs to talk to this npc and handles dialogue accordingly.
    /// </summary>
    /// <param name="accesor">The accessor's questing component.</param>
    /// <returns></returns>
    ActiveDialogue? GetActiveDialogueForDialogueQueststeps(QuestingComponent accesor) {
        List<Quest> accesorQuests = accesor.ActiveQuests;

        if (accesorQuests.Count <= 0)
            return null;

        List<Quest> activeDialogueQuests = accesorQuests
        .Where(x => x.CurrentStep().GetType().Equals(typeof(DialogueQueststep)))?
        .Where(x => (x.CurrentStep().Data as DialogueQueststepSO).Target.GetComponent<NPC>().UID.Equals(Behaviour.GetComponent<NPC>().UID)).ToList();

        if (activeDialogueQuests.Count <= 0)
            return null;

        return  new ActiveDialogue(((DialogueQueststepSO)activeDialogueQuests[0].CurrentStep().Data).Dialogue, () => activeDialogueQuests[0].CurrentStep().Complete());
    }
}