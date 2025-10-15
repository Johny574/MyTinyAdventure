using UnityEngine.UIElements;

public abstract class ItemStackUISlot : UISlot
{
    public Label Count;
    ItemStack stack;
    protected ItemStackUISlot() {
        Count = new Label();
        Count.AddToClassList("stack-label");
        Count.AddToClassList("label");
        Count.name = "Count";
        Add(Count);
        AddToClassList("slot-medium");
    }

    public override void Update<T>(T data) {
        if (data == null)
            return;
        stack = (ItemStack)(object)data;

        Count.text = stack.Count.ToString();
        Icon.style.backgroundImage = new StyleBackground(stack.Item.Sprite);
    }
}