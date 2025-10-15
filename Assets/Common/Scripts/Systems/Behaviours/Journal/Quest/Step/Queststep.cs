using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public abstract class Queststep  {
    public QueststepSO SO;
    protected QuestingComponent _parttaker;
    Quest _quest;

    protected Queststep(QueststepSO data, QuestingComponent parttaker, Quest quest){
        SO = data;
        _parttaker = parttaker;
        _quest = quest;
    }

    public virtual void Complete() {
        _quest.StepComplete(this);
         foreach (var scene in SO.EnabledObjects)
            if (scene.Scene == SceneManager.GetActiveScene().name)
                scene.Objects.ForEach(x => SceneTracker.Instance.GetUnique(x).gameObject.SetActive(true));

        foreach (var scene in SO.DisabledObjects)
            if (scene.Scene == SceneManager.GetActiveScene().name)
                scene.Objects.ForEach(x => SceneTracker.Instance.GetUnique(x).gameObject.SetActive(false));
     
    }

    // void OnComplete() {
    //     if (SO._xpReward <= 0) {
    //         return;
    //     }

    //     // Data.ItemRewards.ForEach(x => new InventoryCommands.AddCommand(x, _parttaker).Execute());
    //     // _parttaker.Service<CurrencyService>().Add(Data.CurrencyRewards);        
    // }

    public abstract Vector2 Closestpoint(Vector2 origin);
}