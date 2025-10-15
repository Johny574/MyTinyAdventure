




using System;
using UnityEngine;

public abstract class StatComponent : Component
{
    public BarData Data;
    public Action<BarData, float> Changed;
    protected StatComponent(MonoBehaviour behaviour) : base(behaviour) {

    }
}