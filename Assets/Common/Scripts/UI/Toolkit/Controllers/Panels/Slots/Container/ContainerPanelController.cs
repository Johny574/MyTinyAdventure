using System;
using UnityEngine;
using UnityEngine.UIElements;

public class ContainerPanelController : SlotPanelController
{
    public Container Container;
    public InventoryBehaviour Player;

    public ContainerPanelController(VisualTreeAsset panel_t, VisualTreeAsset tooltip_t, VisualElement root, bool dragable, AudioSource openaudio, AudioSource closeaudio, VisualTreeAsset ghostIcon_t, VisualTreeAsset stats_t, InventoryBehaviour player) : base(panel_t, tooltip_t, root, dragable, openaudio, closeaudio, ghostIcon_t, stats_t)
    {
    }

    public override void Create(Array data) {
         for (int i = 0; i < data.Length; i++) {
            ContainerUISlot slot = new ContainerUISlot(this);
            DragAndDropManipulator draganddrop = new DragAndDropManipulator(_ghostIcon);

            draganddrop.DropSlot += () =>
            {
                return null;
            };

            draganddrop.DragStart += () => {
                _ghostIcon.Q<VisualElement>("Icon").style.backgroundImage = new StyleBackground(((ItemStack[])data)[i].Item?.Sprite);
            };

            draganddrop.OnDrop += (from, to) => {
                _ghostIcon.style.visibility = Visibility.Hidden;

                if (to == null)
                    return;

                to.OnDrop(((ItemStack[])data)[i]);
            };

            slot.AddManipulator(draganddrop);

            _gridView.hierarchy.Add(slot);
        }
    }

    public override void Setup() {
        ContainerEvents.Instance.Open += (container) => Open(container);
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

    void Open(Container container) {
        Container = container;
        Enable();
        Refresh(container.Inventory.Inventory);
    }
}