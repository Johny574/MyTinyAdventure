


using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Sign : MonoBehaviour, IInteractable
{
    Interactable _interactable;
    public Dialogue ActiveDialogue { get; set; }
    public void CancelTarget() => _interactable.CancelTarget();
    public void Interact(GameObject accesor) => DialogueController.Instance.Open(ActiveDialogue);
    public void Target() => _interactable.Target();

    void Awake() {
        _interactable = GetComponent<Interactable>();
    }

}