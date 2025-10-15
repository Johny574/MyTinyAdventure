
using UnityEngine.UIElements;

[UxmlElement]
public partial class ShopUISlot : ItemStackUISlot
{
    public ShopUISlot() {
        AddToClassList("inventory-slot");
    }

    public override void OnDrop<T>(T data)
    {
        if (typeof(T) != typeof(ItemStack) || data == null)
            return;

        ItemStack item = (ItemStack)(object)data;

        Player.Instance.Inventory.Inventory.Remove(item);
        Player.Instance.Currency.Currency.Add((int)(item.Item.CostPrice * item.Item.SellPercentage));
    }
}