
using System;
using UnityEngine;



[Serializable]
public class HealthGlobeComponent : GlobeComponent
{
    public HealthGlobeComponent(GlobeBehaviour behaviour, float amount) : base(behaviour, amount)
    {
    }


    // public override void Collect(EntityService entity) {
    //     base.Collect(entity);
    // }

    public override void Collect(GameObject collector) {
        collector.GetComponent<HealthBehaviour>().Health.Update((int)Amount);
        Behaviour.gameObject.SetActive(false);
        // new HealthCommands.AddCommand(_amount, entity.Service<HealthService>());
    }

    public void Initilize() {
    }
}