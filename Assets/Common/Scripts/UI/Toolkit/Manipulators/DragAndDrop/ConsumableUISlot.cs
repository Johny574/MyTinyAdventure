using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class ConsumableUISlot : ItemStackUISlot
{
    [UxmlAttribute]
    public int Index;
    VisualElement _fill;
    Consumable _consumable;

    public ConsumableUISlot() {
        AddToClassList("hotbar-slot");

        _fill = new VisualElement();
        _fill.style.backgroundColor = new Color(0f, 0f, 0f, .5f);
        _fill.style.width = new StyleLength(Length.Percent(100));
        _fill.style.height = new StyleLength(Length.Percent(0));
        _fill.name = "fill";
        _fill.style.bottom = new StyleLength(0f);
        _fill.style.alignSelf = new StyleEnum<Align>(Align.Center);
        _fill.style.position = Position.Absolute;
        Add(_fill);
    }

    public void Tick() {
        if (_consumable == null)
            return;
        _fill.style.height = new StyleLength(Length.Percent(_consumable.Fill * 100));
    }

    public override void Update<T>(T data) {
        if (data == null)
            return;
        _consumable = (Consumable)(object)data;
        base.Update(_consumable.Stack);
        
    }

    public override void OnDrop<T>(T data) {
        if (typeof(T) != typeof(ItemStack) || data == null)
            return;

        var _stack = (ItemStack)(object)data;

        if (_stack.Item.GetType() != typeof(ConsumableSO))
            return;

        _consumable = new Consumable(_stack);

        Player.Instance.Inventory.Inventory.Remove(_consumable.Stack);
        Player.Instance.Consumables.Consumables.Add(Index, _consumable.Stack);
    }
}