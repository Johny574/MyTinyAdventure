

/// <summary>
///  Passed into stack controller.
///  Controls the current stack being handled.
///  Eg shopcommand.sellcommand
/// </summary>

public abstract class ItemStackCommand : ICommand
{
    public ItemStack Stack;

    protected ItemStackCommand(ItemStack stack) {
        Stack = stack;
    }

    public abstract void Execute();
}