
using UnityEngine;

public interface IInteractable {
    public abstract void Interact(GameObject accesor);
    public abstract void CancelTarget();
    public abstract void Target();
}