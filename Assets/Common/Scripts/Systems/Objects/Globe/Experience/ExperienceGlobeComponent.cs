using System;
using UnityEngine;

[Serializable]
public class ExperienceGlobeComponent : GlobeComponent
{
    public ExperienceGlobeComponent(GlobeBehaviour behaviour, float amount) : base(behaviour, amount)
    {
    }

    public override void Collect(GameObject collector) {
        collector.GetComponent<ExperienceBehaviour>().Experience.Update(Amount);
        Behaviour.gameObject.SetActive(false);
    }
}