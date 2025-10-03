using System.Linq;
using UnityEngine;

public class CollectItemQueststep : Queststep {
    public ItemStack[] Items;

    public CollectItemQueststep(QueststepData data, QuestingComponent parttaker, Quest quest) : base(data, parttaker, quest) {
        Items = ((CollectItemQuestStepData)data).Items;
        // Item = new Stack<ItemData>(item.Data, 0, item.Counter().Limit);
        // GameEvents.Instance.InventoryEvents.ItemAdded += OnItemCollected;
    }


    public override Vector2 Closestpoint(Vector2 origin) {
        return Vector2.zero;
        // if (GlobalTracker.Instance.Tracker<ItemTracker>().Objects.ContainsKey(typeof(ItemBehaviour))) {
        //     var target = GlobalTracker.Instance.Tracker<ItemTracker>().GetClosestItem(typeof(ItemBehaviour), Item.Data, origin);
        //     if (target != null) {
        //         return target.transform.position;
        //     }
        // }

        // if (GlobalTracker.Instance.Tracker<EntityTracker>().Objects.ContainsKey(typeof(NPCEntityServiceBehaviour))) {
        //     var merchants = GlobalTracker.Instance.Tracker<EntityTracker>().Objects[typeof(NPCEntityServiceBehaviour)].Where(x => x.GetComponent<NPCEntityServiceBehaviour>().Service.Service<InventoryService>().Inventory.Where(x => x != null).Where(x => x.Data != null).Where(x => x.Data.ID == Item.Data.ID).Count() > 0).ToList();
        //     if (merchants.Count() > 0) {
        //         return merchants[0].transform.position;
        //     }
        // }
     
        // return Vector2.zero;
    }

    public void OnItemCollected(ItemStack stack) {
        // if (!(Items.Item == stack.Item)) {
        //     return;
        // }

        // if (Quest.Step() != this) {
        //     return;
        // }

        // Items.Update(stack.Count);

        // if (Items.Count >= ((ItemData)Items.Item).Limit) {
        //     Complete();
        // }
    }

}

