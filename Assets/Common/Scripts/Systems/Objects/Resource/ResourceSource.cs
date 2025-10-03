using System.Linq;
using UnityEngine;



[RequireComponent(typeof(Interactable))]
public class ResourceSource : MonoBehaviour, IInteractable
{
    [SerializeField] ItemStack _resources;
    [SerializeField] int _durability = 5;
    float _dropRate;
    [SerializeField] ToolItemData.Type _toolType;

    void Awake() {
        _dropRate = _resources.Count / _durability;
    }

    void Start() {
        SceneTracker.Instance.Register<Coin>(gameObject);   
    }

    void OnDisable() {
        // SceneTracker.Instance?.Unregister<Coin>(gameObject);   
    }

    public void Interact(GameObject accesor) {
        // var tools = accesor.Service<InventoryService>().Items.Where(x => x.Data.GetType() == typeof(ToolItemData));
        // if (tools.Where(x => ((ToolItemData)x.Data)._Type == _toolType).Count() < 1) {

        //     GameEvents.Instance.ResourceEvents.ToolError?.Invoke();
        //     return;
        // }

        // // todo : play animation, call drop at end
        // Drop(new ItemStack<ItemData>(_resources.Data, _dropRate, _resources.Counter().Limit), transform.position);

        // // durability
        // _durability--;
        // if (_durability <= 0) {
        //     gameObject.SetActive(false);
        //     TileGrid.Instance._resourcesMap.SetTile(TileGrid.Instance._resourcesMap.WorldToCell(transform.position), null);
        // }
    }

    public void CancelTarget() {
        throw new System.NotImplementedException();
    }

    public void Target() {
        throw new System.NotImplementedException();
    }

    // todo : drop
    // async void Drop(ItemStack<ItemData> resource, Vector2 origin) {
    // var obj = await CentralManager.Instance.Manager<ObjectManager>().Pooler(Pooler.Type.Object, "Resource").GetObject();
    // obj.GetComponent<IPoolObject>().Initilize(resource);
    // obj.GetComponent<Dropable>().Drop(origin);
    // }
}