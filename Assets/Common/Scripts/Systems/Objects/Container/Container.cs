
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Container : MonoBehaviour, IInteractable  {

    public InventoryComponent Inventory { get; set; }
    [SerializeField] ItemStack[] _items = new ItemStack[16];

    void Awake() {
        Inventory = new(this, _items);
    }

    public void Interact(GameObject accesor) => ContainerEvents.Instance.Open.Invoke(this);
    public void CancelTarget() => ContainerEvents.Instance.Close.Invoke();
    public void Target() { }
}