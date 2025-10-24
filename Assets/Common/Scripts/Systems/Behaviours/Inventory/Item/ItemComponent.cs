using System;
using UnityEngine;

public class ItemComponent : Component, IPoolObject<ItemStack> {
    public ItemStack Stack { get; set; }
    protected SpriteRenderer _renderer;

    public ItemComponent(ItemBehaviour behaviour, ItemStack stack, SpriteRenderer renderer) : base(behaviour) {
        Stack = stack;
        _renderer = renderer;
    }

    // public void Initilize<T>(T variant) {
    //     Data = (Stack<ItemData>)(object)variant;
    //     _renderer.sprite = Data?.Data?.Icon;
    // }

    public void Interact(GameObject accesor) {
        accesor.gameObject.GetComponent<InventoryBehaviour>().Inventory.Add(Stack);
        // GameEvents.Instance.InteractEvents.Cancel?.Invoke();
    }

    public void Bind(ItemStack variant) {
        Stack = variant;
        _renderer.sprite = variant.Item?.Sprite;
    }

    public void Initilize() {
        _renderer = Behaviour.GetComponentInChildren<SpriteRenderer>();
        _renderer.sprite = Stack.Item?.Sprite; 
    }
}