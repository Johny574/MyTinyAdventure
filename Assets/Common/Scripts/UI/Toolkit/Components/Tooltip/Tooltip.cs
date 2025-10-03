using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class Tooltip : VisualElement
{
    VisualTreeAsset stat_t;

    List<VisualElement> stats;

    public Tooltip() {
        AddToClassList("panel-light");
        AddToClassList("tooltip");
        style.position = Position.Absolute;
        style.display = DisplayStyle.None;
    }

    public void Setup(VisualTreeAsset stats_t) {
        stat_t = stats_t;
        stats = new() {
            stat_t.CloneTree(),
            stat_t.CloneTree(),
            stat_t.CloneTree(),
            stat_t.CloneTree(),
            stat_t.CloneTree(),
            stat_t.CloneTree(),
        };
        stats.ForEach(x => Add(x));
    }

    public void Show(ItemStack item, Vector2 pos) {
        style.left = pos.x;
        style.top = pos.y;
        style.display = DisplayStyle.Flex;
    }

    public void Hide() {
        style.display = DisplayStyle.None;
    }
}