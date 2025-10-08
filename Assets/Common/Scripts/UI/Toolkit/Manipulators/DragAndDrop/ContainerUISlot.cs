



public class ContainerUISlot : ItemStackUISlot
{
    ContainerPanelController _controller;

    public ContainerUISlot(ContainerPanelController controller)
    {
        AddToClassList("inventory-slot");
        _controller = controller;
    }

    public override void OnDrop<T>(T data)
    {
        if (typeof(T) != typeof(ItemStack) || data == null)
            return;

        ItemStack item = (ItemStack)(object)data;

        _controller.Player.Inventory.Remove(item);
        _controller.Container.Inventory.Add(item);
        _controller.Refresh(_controller.Container.Inventory.Inventory);
    }
}