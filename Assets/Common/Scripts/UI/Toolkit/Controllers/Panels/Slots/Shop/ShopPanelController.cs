using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using ShopCommands;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopPanelController : SlotPanelController
{
    ShopComponent _shop;
    VisualElement _cart;
    VisualElement _total;

    public ShopPanelController(VisualTreeAsset panel_t, VisualElement root, bool dragable, AudioSource openaudio, AudioSource closeaudio, VisualTreeAsset ghostIcon_t, VisualTreeAsset stats_t) : base(panel_t, root, dragable, openaudio, closeaudio, ghostIcon_t, stats_t) {
    }

    public override void Setup() {
        _cart = _panel.Q<VisualElement>("Cart");
        _total = _panel.Q<VisualElement>("Currency");
        _panel.Q<Button>("Checkout").clicked += () => _shop?.Checkout();
    }

    public void Close() {
        Disable();
        if (_shop != null) {
            _shop.Cart.CollectionChanged -= UpdateCart;
            _shop.Cart.Clear();
            _cart.hierarchy.Clear();
            _shop = null;
        }
    }

    public void Open(ShopComponent shop, GameObject accesor) {
        _shop = shop;
        _shop.Cart.CollectionChanged += UpdateCart;
        Refresh(shop.Shop.Inventory);
        Enable();
        int index = 0;
        _gridView.BringToFront();
        var children = _gridView.Children().ToArray();
        foreach (var slot in children) {
            BuyCommand shopcommand = new BuyCommand(accesor, shop.Shop.Inventory[index]);
            slot.BringToFront();
            DragAndDropManipulator dragAnddrop = new DragAndDropManipulator(_ghostIcon, () => ItemStackPanelBehaviour.Instance.Open(shopcommand, () => shop.Cart.Add(shopcommand)));
            slot.AddManipulator(dragAnddrop);
            index++;
        }
    }

    // todo : this can me be intergrated into concrete class
    protected override void Update(Array data) {
        int index = 0;
        ItemStack[] items = data as ItemStack[];
        foreach (var slot in _gridView.Children()) {
            ShopUISlot s = slot as ShopUISlot;
            s.Update(items[index]);
            index++;
        }
    }

    void UpdateCart(object sender, NotifyCollectionChangedEventArgs e) {
        _cart.hierarchy.Clear();

        ObservableCollection<ShopCommand> cart = sender as ObservableCollection<ShopCommand>;
        _total.dataSource = _shop.Total;

        foreach (var item in cart) {
            CartUISlot slot = new CartUISlot();
            _cart.hierarchy.Add(slot);
            slot.Update(item.Stack);
        }
    }

    public override void Create(Array data) {
        for (int i = 0; i < data.Length ; i++) {
            ShopUISlot slot = new ShopUISlot();
            _gridView.hierarchy.Add(slot);
        }
    }
}