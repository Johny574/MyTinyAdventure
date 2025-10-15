using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class CraftingPanelController : PanelController
{
    VisualTreeAsset _slot_t;
    CraftingCommands.CraftCommand _handle;
    Label _heading;
    VisualElement _icon;
    List<VisualElement> _recipeSlots;

    public CraftingPanelController(VisualTreeAsset panel_t, VisualElement root, bool dragable, AudioSource closeaudio, AudioSource openaudio, VisualTreeAsset slot_t) : base(panel_t, root, dragable, closeaudio, openaudio)
    {
        _slot_t = slot_t;
    }

    public override void Setup()
    {
        Recipe[] recipes = UnityEngine.Resources.LoadAll<Recipe>("Recipe");
        _heading = _panel.Q<Label>("Head");
        _icon = _panel.Q<VisualElement>("Head-Icon");
        ListView listView = _panel.Q<ListView>("ListView");
        _recipeSlots = _panel.Q<VisualElement>("Recipe-slots").Children().ToList();

        listView.makeItem = () =>
        {
            VisualElement slot = _slot_t.CloneTree().Children().First();
            Label label = slot.Q<Label>("Label");
            VisualElement icon = slot.Q<VisualElement>("Icon");
            slot.Q<Button>().clicked += () => SlotClicked(slot.dataSource as Recipe);
            return slot;
        };

        listView.itemsSource = recipes;

        listView.bindItem = (element, index) => element.dataSource = recipes[index];
        listView.dataSource = recipes;

        Button craftbutton = _root.Q<Button>("Craft");
        craftbutton.clicked += CraftButtonClicked;

        Button cancelbutton = _root.Q<Button>("Craft");
        cancelbutton.clicked += Toggle;
    }

    void SlotClicked(Recipe recipe)
    {
        _handle = new(recipe, Player.Instance.Inventory.Inventory, 0);
        _heading.text = recipe.Result.Item.name;
        _icon.style.backgroundImage = new StyleBackground(recipe.Result.Item.Sprite);

        VisualElement stats = _panel.Q<VisualElement>("Stats");

        if (recipe.Result.Item.GetType().IsAssignableFrom(typeof(GearItemSO)))
            foreach (var statpoint in stats.Children())
                statpoint.style.visibility = Visibility.Hidden;
        else
            foreach (var statpoint in stats.Children())
                statpoint.style.visibility = Visibility.Visible;

        RefreshRecipe(recipe);
    }

    void RefreshRecipe(Recipe recipe)
    {
        Clear();
        int index = 0;
        foreach (var item in recipe.Material)
        {
            _recipeSlots[index].Q<VisualElement>("Icon").style.backgroundImage = new StyleBackground(item.Item.Sprite);
            _recipeSlots[index].Q<Label>("Count").text = item.Count.ToString();
            index++;
        }
    }

    void Clear()
    { 
        foreach (var slot in _recipeSlots)
        {
            slot.Q<VisualElement>("Icon").style.backgroundImage = null;
            slot.Q<Label>("Count").text = "";
        }
    }

    void CraftButtonClicked() => _handle.Execute();
}