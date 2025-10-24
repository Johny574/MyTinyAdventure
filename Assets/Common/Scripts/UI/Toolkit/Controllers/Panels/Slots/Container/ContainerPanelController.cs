using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class ContainerPanelController : SlotPanelController
{
    public Container ContainerHandle { get; private set; }
    VisualElement _inventorySlots, _gearSlots;
    public ContainerPanelController(VisualTreeAsset panel_t, VisualTreeAsset tooltip_t, VisualElement root, bool dragable, AudioSource openaudio, AudioSource closeaudio, VisualTreeAsset ghostIcon_t, VisualTreeAsset stats_t, InventoryBehaviour player) : base(panel_t, tooltip_t, root, dragable, openaudio, closeaudio, ghostIcon_t, stats_t)
    {

    }
    
    public override void Create(Array data)
    {
        for (int i = 0; i < data.Length; i++)
        {
            ContainerUISlot slot = new ContainerUISlot(this);
            DragAndDropManipulator draganddrop = new DragAndDropManipulator(_ghostIcon);

            int index = i;
            draganddrop.DropSlot += DropSlot;

            draganddrop.DragStart += () =>
            {
                _ghostIcon.Q<VisualElement>("Icon").style.backgroundImage = new StyleBackground(((ItemStack[])data)[index].Item?.Sprite);
            };

            draganddrop.OnDrop += (from, to) =>
            {
                _ghostIcon.style.visibility = Visibility.Hidden;

                if (to == null)
                    return;

                to.OnDrop(((ItemStack[])data)[index]);
                ContainerHandle.Inventory.Remove(((ItemStack[])data)[index]);
            };

            slot.AddManipulator(draganddrop);

            slot.RegisterCallback<MouseDownEvent>(evt =>
            {
                if (evt.button != 1) return;
                Player.Instance.Inventory.Inventory.Add(((ItemStack[])data)[index]);
                ContainerHandle.Inventory.Remove(((ItemStack[])data)[index]);
                Refresh(ContainerHandle.Inventory.Inventory);
            });

            _gridView.hierarchy.Add(slot);
        }
    }

    UISlot DropSlot() {
        var gearslot = _root.Q<VisualElement>("GearSlots").Children().SelectMany(x => x.Children()).Where(x => x.worldBound.Contains(this._ghostIcon.worldBound.center)).FirstOrDefault() as UISlot;
        if (gearslot != null)
            return gearslot;

        var inventorySlot = _root.Q<VisualElement>("Inventory").Q<ScrollView>().Children().First().Children().Where(x => x.worldBound.Overlaps(this._ghostIcon.worldBound)).FirstOrDefault() as UISlot;
        if (inventorySlot != null)
            return inventorySlot;

        return null;
    }

    public override void Setup()
    {
        ContainerEvents.Instance.Open += ContainerOpened;
        ContainerEvents.Instance.Close += Disable;
    }

    void ContainerOpened(Container container) => Open(container);
    
    public void OnDisable()
    {
        ContainerEvents.Instance.Open -= ContainerOpened;
        ContainerEvents.Instance.Close -= Disable;
    }

    protected override void Update(Array data) {
        ItemStack[] items = data as ItemStack[];
        int index = 0;
        foreach (ContainerUISlot slot in _gridView.Children()) {
            if (items[index].Count <= 0) {
                slot.Icon.style.display = DisplayStyle.None;
                slot.Count.text = "";
            }
            else {
                slot.Icon.style.backgroundImage = new StyleBackground(items[index].Item?.Sprite);
                slot.Icon.style.display = DisplayStyle.Flex;
                slot.Count.text = items[index].Count.ToString();
            }
            index++;
        }
    }

    void Open(Container container) {
        ContainerHandle = container;
        Enable();
        Refresh(container.Inventory.Inventory);
    }
}