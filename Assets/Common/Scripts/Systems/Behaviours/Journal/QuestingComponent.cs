using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;


public class QuestingComponent : JournalComponent, ISerializedComponent<QuestData[]>
{
    public List<Quest> ActiveQuests { get; set; } = new();
    public List<Quest> Completed { get; set; } = new();
    public Action<Quest> Added, Removed;
    public Action<Queststep> StepCompleted;
    CurrencyComponent _currency;
    InventoryComponent _inventory;

    public QuestingComponent(MonoBehaviour behaviour, List<QuestSO> quests) : base(behaviour, quests) {
        ActiveQuests = quests.Select(x => new Quest(x, this, 0)).ToList();
    }

    public void Initilize(CurrencyComponent currency, InventoryComponent inventory) {
        _currency = currency;
        _inventory = inventory;
    }

    public void Awake() {
        // Quests.ForEach(x => x.Start(x.CurrentStep, gameObject));
    }

    public void Add(QuestSO data) {
        Quests.Remove(data);
        Quest quest = new Quest(data, this);
        ActiveQuests.Add(quest);
        quest.OnCompleted += OnQuestCompleted;
    }

    public void Remove(Quest quest) {
        Quests.Remove(quest.SO);
        ActiveQuests.Remove(quest);
        Removed?.Invoke(quest);
        quest.OnCompleted -= OnQuestCompleted;
    }

    void OnQuestCompleted(Quest quest) {
        Completed.Add(quest);
        _currency.Add(quest.SO.CurrencyRewards);

        // todo : drop these items
        for (int i = 0; i < quest.SO.ItemRewards.Count; i++) {
            try {
                _inventory.Add(quest.SO.ItemRewards[i]);
            }
            catch (InventoryFullException e) {
                Debug.Log(e);
            }
        }

        Remove(quest);
    }

    public QuestData[] Save() {
        return ActiveQuests.Select(x => new QuestData(x.SO.GUID, x.Index)).ToArray();
    }

    public void Load(QuestData[] save) {
        ActiveQuests = new();
        for (int i = 0; i < save.Length; i++) {
            QuestData quest = save[i]; // keep this reference in memory

            UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<QuestSO> QuestSO = Addressables.LoadAssetAsync<QuestSO>(new AssetReference(quest.GUID));
            QuestSO.Completed += (itemso) => {
                if (itemso.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Failed) {
                    throw new System.Exception($"Failed to load asset {quest.GUID}");
                }

                ActiveQuests.Add(new Quest(itemso.Result, this, quest.CurrentStep));
            };
        }

    }

    // public override void Initilize(EntityComponent entity) {
    //     base.Initilize(entity);
    //     JournalAudio.Initilize(entity);
    //     PlayerQuests = new();

    //     foreach (var quest in _startingQuests) {
    //         AddQuest(new Quest(quest.ID, 0, entity), false);
    //     }
    // }

    // public override List<int> Quests {
    //     get => PlayerQuests.Select(x => x.ID).ToList();
    //     set => PlayerQuests = value.Select(x => new Quest(x, 0, Entity)).ToList();
    // }


    // void AddQuest(Quest quest, bool playNotification) {    
    //     IEnumerable<Quest> matchingQuests = PlayerQuests.Where(x => x.ID == quest.ID).ToArray();

    //     foreach (var q in matchingQuests) {
    //         PlayerQuests.Remove(q);
    //         q.Complete();
    //     }
    // quest.OnCompleted += (quest) => JournalAudio.AudioSettings["QuestComplete"].Play();
    // GameEvents.Instance.JournalEvents.Added?.Invoke(Entity, quest, playNotification);
    // GameEvents.Instance.JournalEvents.Update?.Invoke(PlayerQuests.ToArray());
    // }


    // public override T Get<T>() => (T)(object)PlayerQuests.ToDictionary(x => x.Data().ID, x => x.CurrentStep);

    // public override void Set<T>(T obj) {
    //     var quests = (Dictionary<int, int>)(object)obj;
    //     foreach (var quest in quests) {
    //         AddQuest(new Quest(quest.Key, quest.Value, Entity), false);
    //     }
    // }

    // public void Tick() {
    //     foreach (var quest in PlayerQuests) {
    //         if (quest.Step().GetType() == typeof(ReachpointQueststep)) {
    //             ((ReachpointQueststep)quest.Step()).Tick(Entity.Component<Transform>().position);
    //         }
    //     }
    // } 
}