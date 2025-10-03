using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryPanelController : SlotPanelController 
{
    public InventoryPanelController(VisualTreeAsset panel_t, VisualElement root, bool dragable, AudioSource openaudio, AudioSource closeaudio, VisualTreeAsset ghostIcon_t, VisualTreeAsset stats_t) : base(panel_t, root, dragable, openaudio, closeaudio, ghostIcon_t, stats_t) {
    }

    public void Open() {
        Enable();
        Refresh(Player.Instance.Inventory.Inventory.Inventory);
    }
    
    public override void Create(Array data) {
        for (int i = 0; i < data.Length; i++) {
            InventoryUISlot slot = new InventoryUISlot();
            _gridView.hierarchy.Add(slot);
        }
    }

    public override void Setup() {
        InventoryComponent inventory = Player.Instance.Inventory.Inventory;

        Refresh(inventory.Inventory);
        
        VisualElement containerSlots = _root.Q<VisualElement>("ContainerSlots");
        VisualElement shopSlots = _root.Q<VisualElement>("ShopSlots");
        var currency = _panel.Q<VisualElement>("Currency");
        currency.dataSource = Player.Instance.Currency.Currency;
        inventory.Added += (item, inventory) => Update(inventory);
        inventory.Removed += (item, inventory) => Update(inventory);
    }

    protected override void Update(Array data) {
        IList<ItemStack> items = data as IList<ItemStack>;
        int index = 0;
        
        VisualElement gearSlots = _root.Q<VisualElement>("GearSlots");
        VisualElement  hotbar = _root.Q<VisualElement>("Hotbar");

        VisualElement skillslots = hotbar.Q<VisualElement>("Skills");
        VisualElement attackslots= hotbar.Q<VisualElement>("Consumables");
        VisualElement consumableslots = hotbar.Q<VisualElement>("Consumables");

        foreach (InventoryUISlot slot in _gridView.Children()) {
            if (items[index].Count <= 0) {
                slot.Icon.style.display = DisplayStyle.None;
                slot.Count.text = "";
            }
            else {
                slot.Icon.style.backgroundImage = new StyleBackground(items[index].Item?.Sprite);
                slot.Icon.style.display = DisplayStyle.Flex;
                slot.Count.text = items[index].Count.ToString();
            }

            DragAndDropManipulator dragAndDropManipulator = new DragAndDropManipulator(this._ghostIcon);
            dragAndDropManipulator.DropSlot += () => {
                var gearslot = gearSlots.Children().SelectMany(x => x.Children()).Where(x => x.worldBound.Contains(this._ghostIcon.worldBound.center)).FirstOrDefault() as UISlot;
                if (gearslot != null)
                    return gearslot;

                var shopSlot = gearSlots.Children().Where(x => x.worldBound.Overlaps(this._ghostIcon.worldBound)).FirstOrDefault() as UISlot;
                if (shopSlot != null)
                    return shopSlot;

                var containerSlot = gearSlots.Children().Where(x => x.worldBound.Overlaps(this._ghostIcon.worldBound)).FirstOrDefault() as UISlot;
                if (containerSlot != null)
                    return containerSlot;


                var children = consumableslots.Children();
                var consumableSlot = children.Where(x => x.worldBound.Contains(this._ghostIcon.worldBound.center)).FirstOrDefault() as UISlot;
                if (consumableSlot != null)
                    return consumableSlot;

                return null;
            };
            int i = index;

            dragAndDropManipulator.DragStart += () => {
                _ghostIcon.Q<VisualElement>("Icon").style.backgroundImage = new StyleBackground(((ItemStack[])data)[i].Item?.Sprite);
            };

            dragAndDropManipulator.OnDrop += (from, to) => {
                _ghostIcon.style.visibility = Visibility.Hidden;

                if (to == null)
                    return;

                to.OnDrop(((ItemStack[])data)[i]);
            };

            dragAndDropManipulator.DragStop += () => {
                _ghostIcon.style.visibility = Visibility.Hidden;
            };

            slot.AddManipulator(dragAndDropManipulator);
            index++;
        }
    }
}