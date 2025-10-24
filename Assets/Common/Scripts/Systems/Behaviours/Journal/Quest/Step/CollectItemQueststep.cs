using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectItemQueststep : Queststep {
    public ItemStack Items;

    public CollectItemQueststep(QueststepSO data, QuestingComponent parttaker, Quest quest) : base(data, parttaker, quest) {
        Items = ((CollectItemQuestStepData)data).Items;
        parttaker.Behaviour.GetComponent<InventoryBehaviour>().Inventory.Added +=  OnItemCollected;
        // Item = new Stack<ItemData>(item.Data, 0, item.Counter().Limit);
        // GameEvents.Instance.InventoryEvents.ItemAdded += OnItemCollected;
    }

    private void OnItemCollected(ItemStack stack, ItemStack[] arg2)
    {
        Items.Count -= stack.Count;
        if (Items.Count <= 0)
            Complete();
    }

    public override Vector2 Closestpoint(Vector2 origin) {
        var currentScene = SceneManager.GetActiveScene().name;
        if (SO.Scene != currentScene) {
            var path = LocationManager.Instance.BFS(currentScene, SO.Scene);
            return SceneTracker.Instance.Objects[typeof(TravelPoint)].Find(x => x.GetComponent<TravelPoint>().Destination.Equals(path[1])).transform.position;
        }
        
        if (SceneTracker.Instance.Objects.ContainsKey(typeof(ItemBehaviour))) {
            var items = SceneTracker.Instance.Objects[typeof(ItemBehaviour)].Where(x => x.GetComponent<ItemBehaviour>().Item.Stack.Item == Items.Item).ToList();
            var target = SceneTracker.Instance.GetClosestObject(items, origin);
            if (target != null) {
                return target.transform.position;
            }
        }

        // if (SceneTracker.Instance.Objects.ContainsKey(typeof(Entity))) {
        //     var merchants = SceneTracker.Instance.Objects[typeof(Entity)].Where(x => x.GetComponent<Entity>().GetComponent<InventoryBehaviour>().Inventory?.Inventory.Where(x => x.Item != null).Where(x => x.Item?.UID == Items.Item?.UID).Count() > 0).ToList();
        //     if (merchants.Count() > 0) {
        //         return merchants[0].transform.position;
        //     }
        // }
     
        return Vector2.zero;
    }
}

