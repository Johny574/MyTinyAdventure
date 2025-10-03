using System.Linq;
using NUnit.Framework;
using UnityEngine;

public class InventoryTests
{
    InventoryComponent _inventory;
    ItemStack _mockItem;
    ItemSO _itemdata;
    int _itemAmount = 8, _itemLimit = 16;

    [SetUp]
    public void SetUp() {
        _inventory = new InventoryComponent(null, new ItemStack[16]);

        _itemdata = ScriptableObject.CreateInstance<ItemSO>();
        _itemdata.Limit = _itemLimit;

        _mockItem = new ItemStack(_itemdata, _itemLimit);
        _inventory.Add(_mockItem);
    }

    [Test]
    public void StackAdded() { 
        ItemStack addedStack = new ItemStack(_itemdata, _itemAmount);
        _inventory.Add(addedStack);
        Assert.That(_inventory.Inventory.Where(x => x.Item == _itemdata).ToList().Count > 1);
    }

    [Test]
    public void StackRemoved() {
        _inventory.Remove(_mockItem);
        Assert.That(!_inventory.Inventory.Contains(_mockItem));
    }

    [Test]
    public void CountChanged() { 
        ItemStack addedStack = new ItemStack(_itemdata, _itemLimit);
        _inventory.Add(addedStack);
        Assert.AreEqual(_inventory.Inventory.ToList().Find(x => x.Item == _itemdata).Count, _itemLimit);
    }

    [Test]
    public void ItemsDroppedOnDeath() {

    }


    [TearDown]
    public void TearDown() {
        
    }
}