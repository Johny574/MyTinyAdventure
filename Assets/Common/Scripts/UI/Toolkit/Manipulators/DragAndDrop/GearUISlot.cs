



using UnityEngine.UIElements;

[UxmlElement]
public partial class GearUISlot : UISlot 
{
    public GearUISlot() {
        AddToClassList("gear-slot");
        AddToClassList("slot-small");
        Icon.AddToClassList("slot-icon");
    }

    public override void OnDrop<T>(T data) {
        if (typeof(T) != typeof(ItemStack) || data == null)
            return;

        ItemStack item = (ItemStack)(object)data;

        if (item.Item.GetType().IsInstanceOfType(typeof(GearItemSO)))
            return;

        Player.Instance.Gear.Gear.Equipt(item.Item);
    }

    public override void Update<T>(T data) {
        ItemSO item = (ItemSO)(object)data;
        if (item != null) {
            Icon.style.backgroundImage = new StyleBackground(item.Sprite);
            Icon.style.display = DisplayStyle.Flex;
        }
        else {
            Icon.style.display = DisplayStyle.None;
        }   
    }
}