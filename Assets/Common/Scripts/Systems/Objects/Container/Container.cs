
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Container : MonoBehaviour, IInteractable  {

    public InventoryComponent Inventory { get; set; }
    [SerializeField] ItemStack[] _items = new ItemStack[16];

    void Awake() {
        Inventory = new(this, _items);
    }

    public void Interact(GameObject accesor) {
        ContainerEvents.Instance.Open.Invoke(this);
    }

    public void CancelTarget() {
        ContainerEvents.Instance.Close.Invoke();
    }

    public void Target() {
    }

    // void OnDisable() {
    //     Serializer.SaveFile(_inventory.Get<ItemStack<int>[]>(), "Storage.json", SaveSlot.AutoSave);
    // }

    // protected override void Start() {
    //     base.Start();
    //     if (Serializer.ContainsSave(SaveSlot.AutoSave, "Storage", ".json")) {
    //         var inventory = Serializer.LoadFile<ItemStack<int>[]>("Storage.json", SaveSlot.AutoSave);
    //         _inventory.Set(inventory);
    //     }
    // }
}