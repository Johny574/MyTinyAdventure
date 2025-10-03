using System;
using UnityEngine;

[Serializable]
public class ExperienceGlobeComponent : GlobeComponent
{
    public ExperienceGlobeComponent(GlobeBehaviour behaviour) : base(behaviour) {
    }

    // public override void Collect(EntityService collector) {
    //     base.Collect(collector);
    //     new ExperienceCommands.AddCommand(amount, collector).Execute();
    // }
    public override void Collect(GameObject collector) {
        collector.GetComponent<ExperienceBehaviour>().Experience.Update(_amount);
        Behaviour.gameObject.SetActive(false);
    }
}