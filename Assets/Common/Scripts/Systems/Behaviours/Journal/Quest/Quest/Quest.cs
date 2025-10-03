using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Quest {
    public QuestSO SO;
    public List<Queststep> Steps { get; private set; }
    public int Index { get; private set; }
    [NonSerialized] public Action<Quest> OnCompleted;
    public bool Completed { get; private set; } = false;
    IPoolObject<PointerData> Pointer;
    QuestingComponent _parttaker;

    public Quest(QuestSO data, QuestingComponent parttaker, int stepindex = 0) {
        Index = stepindex;
        SO = data;
        Steps = new();
        _parttaker = parttaker;
        Steps = SO.Steps.Select(x => QuestFactory.Queststeps[x.GetType()].Invoke(x, parttaker, this)).ToList();
        CreatePointer();
    }

    async void CreatePointer() {
        Pointer = await PointerFactory.Instance.Pool.GetObject<PointerData>();
        Pointer.Bind(new PointerData(_parttaker.Behaviour.gameObject, CurrentStep().Closestpoint(_parttaker.Behaviour.transform.position)));
    }

    public Queststep CurrentStep() => Steps[Index];

    public void StepComplete(Queststep step) {

        if (step != CurrentStep())
            return;

        Index++;
        if (Index >= Steps.Count) {
            Complete();
            ((MonoBehaviour)Pointer).gameObject.SetActive(false);
        }
        else {
            Pointer.Bind(new PointerData(_parttaker.Behaviour.gameObject, CurrentStep().Closestpoint(_parttaker.Behaviour.transform.position)));
        }
        _parttaker.StepCompleted?.Invoke(step);
    }

    public void Complete() {
        Completed = true;
        OnCompleted?.Invoke(this);
    }
}