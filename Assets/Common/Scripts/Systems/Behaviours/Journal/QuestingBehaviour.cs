

using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InventoryBehaviour))]
[RequireComponent(typeof(CurrencyBehaviour))]
public class QuestingBehaviour : MonoBehaviour
{
    public QuestingComponent Questing { get; set; }
    [SerializeField] List<QuestSO> _quests = new();

    void Awake()
    {
        Questing = new(this, _quests);
    }

    void Start()
    {
        Questing.Initilize();
    }
}