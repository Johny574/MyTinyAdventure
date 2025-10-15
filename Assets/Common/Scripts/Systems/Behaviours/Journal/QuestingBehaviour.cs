

using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InventoryBehaviour))]
[RequireComponent(typeof(CurrencyBehaviour))]
public class QuestingBehaviour : MonoBehaviour
{
    public QuestingComponent Questing { get; set; }
    [SerializeField] List<QuestSO> _quests = new();
    [SerializeField] List<QuestSO> _beginnerQuests = new();
    void Awake()
    {
        Questing = new(this);
        GameManager.Instance.OnNewGame += () =>
        {
            _beginnerQuests.ForEach(x => Questing.Add(x));
        };
    }

    void Start() {
        Questing.Initilize(_quests);
    }
}