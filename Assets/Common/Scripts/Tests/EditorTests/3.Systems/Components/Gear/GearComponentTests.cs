using System.Linq;
using NUnit.Framework;
using UnityEngine;

public class GearTests {

    InventoryComponent _inventory;
    GearComponent _gear;
    
    StatpointsComponent _stats;
    GearItemSO _mockItem;

    GearItemSO.Slot _mockItemSlot;
    StatPoints _mockItemStats;

    [SetUp]
    public void SetUp() {
        _mockItemStats = new(new(){ { StatPoints.Stat.Strength, 1 } }, new(){ { StatPoints.Stat.Melee, 2 } }, new(){ { StatPoints.Stat.Magic, 3 } });
        _mockItemSlot = GearItemSO.Slot.Primary;

        _mockItem = ScriptableObject.CreateInstance<GearItemSO>();
        _mockItem.Target = _mockItemSlot;
        _mockItem.Stats = _mockItemStats;

        GearSlots gear = new GearSlots{ { _mockItemSlot, new Gearslot(null, null, null)} };

        _gear = new GearComponent(null, gear, null);

        StatPoints stats = new(new(){ { StatPoints.Stat.Strength, 0 } }, new(){ { StatPoints.Stat.Melee, 0 } }, new(){ { StatPoints.Stat.Magic, 0 } });
        ExperienceComponent xp = new(null, new(), 0);
        _stats = new(null, stats, xp);

        _inventory = new InventoryComponent(null, new ItemStack[16]);

        _inventory.Add(new ItemStack(_mockItem, 1));
    }

    [Test]
    public void ItemRemovedFromInventoryOnEquipt() {
        _gear.Equipt(_mockItem);
        Assert.Zero(_inventory.Inventory.Where(x => x.Item == _mockItem).Count());
    }

    [Test]
    public void ItemAddedToInventoryOnUnEquipt() { 
        _gear.Equipt(_mockItem);
        Assert.Zero(_inventory.Inventory.Where(x => x.Item == _mockItem).Count());

        _gear.UnEquipt(_mockItemSlot);
        Assert.NotZero(_inventory.Inventory.Where(x => x.Item == _mockItem).Count());
    }

    [Test]
    public void StatsChangedOnEquipt() { 
        _gear.Equipt(_mockItem);
        Assert.AreEqual(_stats.StatPoints.BaseStats[StatPoints.Stat.Strength], _mockItem.Stats.BaseStats[StatPoints.Stat.Strength]);
        Assert.AreEqual(_stats.StatPoints.AttackStats[StatPoints.Stat.Melee], _mockItem.Stats.AttackStats[StatPoints.Stat.Melee]);
        Assert.AreEqual(_stats.StatPoints.DefenseStats[StatPoints.Stat.Magic], _mockItem.Stats.DefenseStats[StatPoints.Stat.Magic]);
    }

    [Test]
    public void StatsChangedOnUnEquipt() { 
        _gear.Equipt(_mockItem);
        _gear.UnEquipt(_mockItemSlot);

        Assert.Zero(_stats.StatPoints.BaseStats[StatPoints.Stat.Strength]);
        Assert.Zero(_stats.StatPoints.AttackStats[StatPoints.Stat.Melee]);
        Assert.Zero(_stats.StatPoints.DefenseStats[StatPoints.Stat.Magic]);
    }

    // Todo : move to play mode tests
    [Test]
    public void SpriteChangedOnEquipt() {
        Assert.Fail();
    }
}