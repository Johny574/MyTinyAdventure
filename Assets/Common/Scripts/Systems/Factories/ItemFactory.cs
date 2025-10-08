using UnityEngine;

public class ItemFactory : Factory<ItemFactory, ItemStack>
{
    public async void DropItem(Vector2 origin, ItemStack item)
    {
        IPoolObject<ItemStack> obj = await GetObject(item);
        ((MonoBehaviour)obj).GetComponent<Dropable>().Drop(origin);
    }
}