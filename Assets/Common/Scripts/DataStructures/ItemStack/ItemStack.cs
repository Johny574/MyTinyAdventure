using System;
using UnityEngine;

[Serializable]
public struct ItemStack
{
    public ItemSO Item;
    public int Count;

    public ItemStack(ItemSO item, int counter = 0) {
        Item = item;
        Count = counter;
    }

    public void Update(int amount) => Count += amount;
    public void Set(int amount) => Count = amount;
}