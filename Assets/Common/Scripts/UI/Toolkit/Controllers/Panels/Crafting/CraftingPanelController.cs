using UnityEngine;
using UnityEngine.UIElements;

public class CraftingPanelController : PanelController
{
    VisualTreeAsset _slot_t;
    CraftingCommands.CraftCommand _handle;

    public CraftingPanelController(VisualTreeAsset panel_t, VisualElement root, bool dragable, AudioSource closeaudio, AudioSource openaudio, VisualTreeAsset slot_t) : base(panel_t, root, dragable, closeaudio, openaudio) {
        _slot_t = slot_t;
    }

    public override void Setup() {
        Recipe[] recipes = UnityEngine.Resources.LoadAll<Recipe>("Recipe");

        ListView listView = _panel.Q<ListView>("Slots-view");
        listView.makeItem = () => {
            VisualElement slot = _slot_t.CloneTree();
            slot.Q<Button>().clicked += () => _handle = new((Recipe)slot.dataSource, Player.Instance.Inventory.Inventory, 0);
            return slot;
        };
        listView.itemsSource = recipes;

        listView.bindItem = (element, index) => element.dataSource = recipes[index];
        listView.dataSource = recipes;

        Button craftbutton = _root.Q<Button>("Craft");
        craftbutton.clicked += CraftClicked;
    }

    void CraftClicked() => _handle.Execute();
}