
using System;
using UnityEngine;



[Serializable]
public class HealthGlobeComponent : GlobeComponent
{
    public HealthGlobeComponent(GlobeBehaviour behaviour, float amount, SpriteRenderer color) : base(behaviour, amount, color)
    {
        color.color = Color.red;
    }

    public override void Collect(GameObject collector) {
        collector.GetComponent<HealthBehaviour>().Health.Update((int)Amount);
        Behaviour.gameObject.SetActive(false);
        // new HealthCommands.AddCommand(_amount, entity.Service<HealthService>());
    }

    public void Initilize() {
    }
}