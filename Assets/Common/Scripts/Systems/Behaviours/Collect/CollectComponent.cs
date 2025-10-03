
using System;
using UnityEngine;

public class CollectComponent : Component
{
    public CollectComponent(CollectBehaviour behaviour) : base(behaviour) {
    }

    public void OnTriggerEnter2D(Collider2D col) {
        col.GetComponent<ICollectable>().Collect(Behaviour.gameObject);
    }
}