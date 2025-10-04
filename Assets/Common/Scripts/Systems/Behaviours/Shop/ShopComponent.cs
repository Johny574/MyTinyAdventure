

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using ShopCommands;
using UnityEngine;

public class ShopComponent : Component
{
    public InventoryComponent Shop { get; set; }
    public ObservableCollection<ShopCommand> Cart { get; set; }
    string[] _dialogue;
    public Currency Total;

    public List<ItemStack> Items {
        get => Shop.Inventory.Where(x => x.Item != null).ToList();
    }

    public Dialogue Dialogue(GameObject accesor) {
        return new Dialogue(_dialogue, () => ShopEvents.Instance.Open.Invoke(this, accesor));
    }

    public ShopComponent(MonoBehaviour behaviour, ItemStack[] items, string[] dialogue) : base(behaviour) {
        Shop = new(behaviour, items);
        Cart = new();
        _dialogue = dialogue;
        Total = new(0);
        Cart.CollectionChanged += CartChanged;
    }

    private void CartChanged(object sender, NotifyCollectionChangedEventArgs e) {
        ObservableCollection<ShopCommand> cart = sender as ObservableCollection<ShopCommand>;
        Total = new(cart.Sum(x => x.Stack.Item.CostPrice * x.Stack.Count));
        Debug.Log(Total);
    }

    public void Checkout() {
        for (int i = 0; i < Cart.Count; i++) {
            try {
                Cart[i].Execute();
            }
            catch (Exception e) {
                Debug.Log(e.Message);
                break;
            }
        }
        Cart.Clear();
    }
}