




using System;
using UnityEngine;

public abstract class StatComponent : Component
{
    public BarData Data;
    public Action<BarData> Changed;

    protected StatComponent(MonoBehaviour behaviour) : base(behaviour) {

    }
}