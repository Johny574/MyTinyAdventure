
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Container : MonoBehaviour, IInteractable, IUnique  {

    public InventoryComponent Inventory { get; set; }
    [SerializeField] ItemStack[] _items = new ItemStack[16];
    public int UID { get => _id;  }
    [SerializeField] int _id;

#if UNITY_EDITOR
    void OnValidate()
    {
        if (_id != 0) return;
        _id = Mathf.Abs(System.Guid.NewGuid().GetHashCode());
        UnityEditor.EditorUtility.SetDirty(this);
    }
#endif

    void Awake() {
        Inventory = new(this, _items);
    }

    void Start() {
        SceneTracker.Instance.RegisterUnique<Container>(gameObject, UID);
    }

    public void Interact(GameObject accesor) => ContainerEvents.Instance.Open.Invoke(this);
    public void CancelTarget() => ContainerEvents.Instance.Close.Invoke();
    public void Target() { }
}