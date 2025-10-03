using System;
using UnityEngine;

[Serializable]
public abstract class Queststep  {
    public QueststepData Data;
    protected QuestingComponent _parttaker;
    Quest _quest;

    protected Queststep(QueststepData data, QuestingComponent parttaker, Quest quest){
        Data = data;
        _parttaker = parttaker;
        _quest = quest;
    }

    public virtual void Complete() {
        _quest.StepComplete(this);
        OnComplete();
    }

    private void OnComplete() {
        if (Data._xpReward <= 0) {
            return;
        }

        for (int i = 0; i < (Data._xpReward / 50) - (Data._xpReward % 50); i++) {
            // new ExperienceCommands.DropCommand(50, _parttaker.Component<Transform>().position).Execute();
        }

        // Data.ItemRewards.ForEach(x => new InventoryCommands.AddCommand(x, _parttaker).Execute());
        // _parttaker.Service<CurrencyService>().Add(Data.CurrencyRewards);        
    }

    public abstract Vector2 Closestpoint(Vector2 origin);
}