


using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class DialogueBehaviour : MonoBehaviour, IInteractable
{
    Interactable _interactable;
    DialogueComponent _dialogue;
    [SerializeField] Sprite _questEmote, _dialogueEmote;
    [SerializeField] ActiveDialogue _defaultAction = new(new string[0], null);

    public void CancelTarget() {
        _interactable.CancelTarget();
        _dialogue.CancelTarget();
    }

    public void Interact(GameObject accesor) => _dialogue.Interact(accesor);
    public void Target() => _interactable.Target();

    void Awake() {
        _interactable = GetComponent<Interactable>();
        _dialogue = new(this, _defaultAction,GetComponent<EmoteBehaviour>().Emotes, _questEmote, _dialogueEmote);
    }
    void Start() {
        _dialogue.Initilize(GetComponent<JournalBehaviour>().Journal);
    }
}