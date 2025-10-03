

public class CartUISlot : ItemStackUISlot 
{
    public CartUISlot() {
        AddToClassList("inventory-slot");   
    }

    public override void OnDrop<T>(T data) {
    }
}