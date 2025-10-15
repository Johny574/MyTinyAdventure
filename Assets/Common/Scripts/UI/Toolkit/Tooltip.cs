using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class TooltipBehaviour : Singleton<TooltipBehaviour> {
    [SerializeField] VisualTreeAsset _tooltip_t;
    VisualElement _tooltip;
    [SerializeField] UIDocument _uiDocument;

    protected override void Awake()
    {
        base.Awake();
        var tooltip = _tooltip_t.CloneTree().Children().First();
        _uiDocument.rootVisualElement.Add(tooltip);
        tooltip.style.visibility = Visibility.Hidden;
    }

    public void Show(Vector2 position, ItemSO item)
    {
        UpdateTooltip(item);
        _tooltip.style.left = position.x;
        _tooltip.style.top = position.y;
        _tooltip.style.visibility = Visibility.Visible;
        _tooltip.BringToFront();
    }

    public void UpdateTooltip(ItemSO item)
    {
        var stats = _tooltip.Q<VisualElement>("Stats");
        
        if (item.GetType().IsAssignableFrom(typeof(GearItemSO)))
        {
            var i = item as GearItemSO;
            foreach (var key in i.Stats.AttackStats.Keys) {
                var statpoint = stats.Q<VisualElement>($"{key} Attack");
                statpoint.Q<Label>("Key").text = $"{key} Attack";
                statpoint.Q<Label>("Value").text = i.Stats.AttackStats[key].ToString();
            }

            foreach (var key in i.Stats.DefenseStats.Keys) {
                var statpoint = stats.Q<VisualElement>($"{key} Defense");
                statpoint.Q<Label>("Key").text = $"{key} Defense";
                statpoint.Q<Label>("Value").text = i.Stats.DefenseStats[key].ToString();
            }
        }
        else
        {
            foreach (var child in stats.Children())
            {
                child.style.display = DisplayStyle.None;
            }
        }
    }
    
    public void Hide()
    {
        _tooltip.style.visibility = Visibility.Hidden;
    }
}