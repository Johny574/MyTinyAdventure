using System;
using UnityEngine;


[Serializable]
public abstract class GlobeComponent : Component, ICollectable
{
    public float Amount { get; protected set; }

    protected GlobeComponent(GlobeBehaviour behaviour, float amount) : base(behaviour) {
        Amount = amount;
    }

    public abstract void Collect(GameObject collector);
    
    public enum Type
    {
        Health,
        Experience
    }
}