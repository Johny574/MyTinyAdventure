using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryPanelController : SlotPanelController
{
    VisualElement _containerSlots, _shopSlots, _gearSlots;
    VisualElement _consumableslots;
    ContainerPanelBehaviour _containerPanel;
    ShopPanelBehaviour _shopPanel;

    public InventoryPanelController(VisualTreeAsset panel_t, VisualTreeAsset tooltip_t, VisualElement root, bool dragable, AudioSource openaudio, AudioSource closeaudio, VisualTreeAsset ghostIcon_t, VisualTreeAsset stats_t, ContainerPanelBehaviour containerPanel, ShopPanelBehaviour shopPanel) : base(panel_t, tooltip_t, root, dragable, openaudio, closeaudio, ghostIcon_t, stats_t)
    {
        _containerPanel = containerPanel;
        _shopPanel = shopPanel;
    }

    public override void Open()
    {
        base.Open();
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

        _containerSlots = _root.Q<VisualElement>("Container").Q<ScrollView>("ScrollView");
        _shopSlots = _root.Q<VisualElement>("Shop").Q<ScrollView>("ScrollView");
        var hotbar = _root.Q<VisualElement>("Hotbar");
        _gearSlots = _root.Q<VisualElement>("GearSlots");
        _consumableslots = hotbar.Q<VisualElement>("Consumables");

        var currency = _panel.Q<VisualElement>("Currency");
        currency.dataSource = Player.Instance.Currency.Currency;
        inventory.Added += (item, inventory) => Refresh(inventory);
        inventory.Removed += (item, inventory) => Refresh(inventory);
    }

    protected override void Update(Array data) {
        IList<ItemStack> items = data as IList<ItemStack>;
        var slots = _gridView.Children().Cast<InventoryUISlot>().ToList();
        int index = 0;

        // todo this needs to loop over the array rather
        foreach (var item in items)
        {
            if (item.Count <= 0)
            {
                slots[index].Icon.style.display = DisplayStyle.None;
                slots[index].Count.text = "";
            }
            else
            {
                slots[index].Icon.style.backgroundImage = new StyleBackground(item.Item?.Sprite);
                slots[index].Icon.style.display = DisplayStyle.Flex;
                slots[index].Count.text = item.Count.ToString();
            }
            DragAndDropManipulator dragAndDropManipulator = new DragAndDropManipulator(this._ghostIcon);
            dragAndDropManipulator.DropSlot += DropSlot;

            dragAndDropManipulator.DragStart += () =>
            {
                _ghostIcon.Q<VisualElement>("Icon").style.backgroundImage = new StyleBackground(item.Item?.Sprite);
            };

            dragAndDropManipulator.OnDrop += (from, to) =>
            {
                _ghostIcon.style.visibility = Visibility.Hidden;

                if (to == null)
                    return;
                    
                to.OnDrop(item);
            };

            dragAndDropManipulator.DragStop += () =>
            {
                _ghostIcon.style.visibility = Visibility.Hidden;
            };

            slots[index].RegisterCallback<MouseDownEvent>(evt =>
            {
                if (evt.button != (int)MouseButton.RightMouse)
                    return;

                Player.Instance.Inventory.Inventory.Remove(item);

                if (_containerPanel.Panel.Enabled)
                {
                    _containerPanel.Panel.ContainerHandle.Inventory.Add(item);
                   _containerPanel.Panel.Refresh(_containerPanel.Panel.ContainerHandle.Inventory.Inventory); 
                }

                else if (_shopPanel.Panel.Enabled)
                    Player.Instance.Currency.Currency.Add((int)(item.Item.CostPrice * item.Item.SellPercentage));
                    
                else
                    ItemFactory.Instance.DropItem(Player.Instance.transform.position, item);

                evt.StopPropagation();
            });

            slots[index].AddManipulator(dragAndDropManipulator);
            index++;
        }
    }

    UISlot DropSlot() {
        var gearslot = _gearSlots.Children().SelectMany(x => x.Children()).Where(x => x.worldBound.Contains(this._ghostIcon.worldBound.center)).FirstOrDefault() as UISlot;
        if (gearslot != null)
            return gearslot;

        var shopSlot = _shopSlots.Children().Where(x => x.worldBound.Overlaps(this._ghostIcon.worldBound)).FirstOrDefault() as UISlot;
        if (shopSlot != null)
            return shopSlot;

        var containerSlot = _containerSlots.Children().First().Children().Where(x => x.worldBound.Overlaps(this._ghostIcon.worldBound)).FirstOrDefault() as UISlot;
        if (containerSlot != null)
            return containerSlot;

        var consumableSlot = _consumableslots.Children().Where(x => x.worldBound.Contains(this._ghostIcon.worldBound.center)).FirstOrDefault() as UISlot;
        if (consumableSlot != null)
            return consumableSlot;

        return null;
    } 
}