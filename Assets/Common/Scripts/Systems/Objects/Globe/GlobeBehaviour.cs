



using UnityEngine;

public class GlobeBehaviour : MonoBehaviour, ICollectable
{
    [SerializeField] protected GlobeComponent _globe;
    [SerializeField] GlobeComponent.Type _type;

    protected void Awake() {
        _globe = GlobeFactory.Globes[_type].Invoke(this);
    }

    public void Collect(GameObject collector) {
        _globe.Collect(collector);
    }
}