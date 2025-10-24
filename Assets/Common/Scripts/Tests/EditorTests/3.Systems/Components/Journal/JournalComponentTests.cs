using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

public class JournalTests
{
    [Header("Quest")]
    QuestSO _mockQuest;

    [Header("Steps")]
    QueststepSO _mockQuestStep;
    List<QueststepSO> _mockQuestSteps;

    [Header("Rewards")]
    ItemSO _mockItemData;
    List<ItemStack> _mockItemRewards;
    int _mockCurrencyReward;

    [Header("Components")]
    QuestingComponent _journal;
    CurrencyComponent _currency;
    InventoryComponent _inventory;

    [SetUp]
    public void SetUp() {
        _mockQuest = ScriptableObject.CreateInstance<QuestSO>();
        _mockItemData = ScriptableObject.CreateInstance<ItemSO>();
        _mockQuestStep = ScriptableObject.CreateInstance<CollectItemQuestStepData>();
        _mockCurrencyReward = 500;

        _mockItemRewards = new List<ItemStack>() {
            new ItemStack(_mockItemData, 1),
            new ItemStack(_mockItemData, 1)
        };

        _mockQuestSteps = new() {
            _mockQuestStep,
            _mockQuestStep,
            _mockQuestStep,
        };

        _mockQuest.Steps = _mockQuestSteps;

        _currency = new CurrencyComponent(null);

        _inventory = new InventoryComponent(null, new ItemStack[16]);

        _journal = new QuestingComponent(null);
        _journal.Initilize(new());

        _journal.Add(_mockQuest);
        _journal.Add(_mockQuest);
        _journal.Add(_mockQuest);
    }

    [Test]
    public void Added() {
        Assert.NotZero(_journal.ActiveQuests.Count);
    }

    [Test]
    public void Removed() {
        var targetQuest = _journal.ActiveQuests[Random.Range(0, _journal.ActiveQuests.Count)];
        _journal.Remove(targetQuest);
        Assert.IsTrue(!_journal.ActiveQuests.Contains(targetQuest));
    }

    [Test]
    public void AddedToCompleted() {
        var targetQuest = _journal.ActiveQuests[Random.Range(0, _journal.ActiveQuests.Count)];
        targetQuest.Complete();
        Assert.IsTrue(_journal.Completed.Contains(targetQuest));
    }

    [Test]
    public void RemovedOnComplete() {
        var targetQuest = _journal.ActiveQuests[Random.Range(0, _journal.ActiveQuests.Count)];
        targetQuest.Complete();
        Assert.IsTrue(!_journal.ActiveQuests.Contains(targetQuest));
    }

    [Test]
    public void CurrencyRewardAddedOnCompleted() {
        var targetQuest = _journal.ActiveQuests[Random.Range(0, _journal.ActiveQuests.Count)];
        targetQuest.Complete();
        Assert.AreEqual(_currency.Currency.TotalCopper, _mockCurrencyReward);
    }

    [Test]
    public void ItemRewardsAddedOnCompleted() {
        var targetQuest = _journal.ActiveQuests[Random.Range(0, _journal.ActiveQuests.Count)];
        targetQuest.Complete();

        foreach (var item in _mockItemRewards) {
            Assert.That(_inventory.Inventory.Where(x => x.Item == item.Item).Count() > 0);
        }
    }

    // TODO : move under quest tests
    // [Test]
    // public void QuestCompletedOnAllStepsCompleted()
    // {
        // for (int i = 0; i < _quest.StepsID.Count; i++)
        // {
        //     _quest.Steps[i].Complete();
        // }

        // Assert.IsTrue(_quest.Completed);
    // }
}