using UnityEngine.UIElements;

[UxmlElement]
public partial class AttackUISlot : UISlot
{
    [UxmlAttribute]
    public GearItemSO.Slot Slot;
    public AttackUISlot() {
        AddToClassList("hotbar-slot");
        AddToClassList("slot-medium");
    }

    public override void OnDrop<T>(T data) {

        if (typeof(T) != typeof(ItemStack) || data == null)
            return;

        ItemStack item = (ItemStack)(object)data;

        if (item.Item.GetType() != typeof(GearItemSO))
            return;
    }

    public override void Update<T>(T data) {
        WeaponItemSO item = data as WeaponItemSO;
        if (item == null)
            return;
        
        Icon.style.backgroundImage = new StyleBackground(item.HotbarIcon);
    }
}