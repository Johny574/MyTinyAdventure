using System;
using UnityEngine;
using UnityEngine.UIElements;

public class ContainerPanelController : SlotPanelController
{
    public ContainerPanelController(VisualTreeAsset panel_t, VisualElement root, bool dragable, AudioSource openaudio, AudioSource closeaudio, VisualTreeAsset ghostIcon_t, VisualTreeAsset stats_t) : base(panel_t, root, dragable, openaudio, closeaudio, ghostIcon_t, stats_t) {
    }

    public override void Create(Array data) {
         for (int i = 0; i < data.Length; i++) {
            ContainerUISlot slot = new ContainerUISlot();
            _gridView.hierarchy.Add(slot);
        }
    }

    public override void Setup() {
        ContainerEvents.Instance.Open += (container) => Open(container.Inventory);
        ContainerEvents.Instance.Close += Disable;
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

    void Open(InventoryComponent inventory) {
        Enable();
        Refresh(inventory.Inventory);
    }
}