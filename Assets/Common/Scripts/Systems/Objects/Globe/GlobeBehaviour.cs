using System;
using UnityEngine;

public class GlobeBehaviour : MonoBehaviour, IPoolObject<GlobeData>, ICollectable
{
    [SerializeField] protected GlobeComponent _globe;
    [SerializeField] SpriteRenderer _color;

    public void Collect(GameObject collector) {
        _globe.Collect(collector);
    }

    public void Bind(GlobeData variant)
    {
        if (variant.Type.Equals(GlobeComponent.Type.Health))
            _globe = new HealthGlobeComponent(this, variant.Amount, _color);
        else
            _globe = new ExperienceGlobeComponent(this, variant.Amount, _color);
    }
}

[Serializable]
public struct GlobeData
{
    public GlobeComponent.Type Type;
    public float Amount;

    public GlobeData(GlobeComponent.Type type, float amount)
    {
        Type = type;
        Amount = amount;
    }
}