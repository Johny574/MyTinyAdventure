public partial class ContainerUISlot: ItemStackUISlot
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

        _controller.ContainerHandle.Inventory.Add(item);
        Player.Instance.Inventory.Inventory.Remove(item);
        _controller.Refresh(_controller.ContainerHandle.Inventory.Inventory);
    }
}