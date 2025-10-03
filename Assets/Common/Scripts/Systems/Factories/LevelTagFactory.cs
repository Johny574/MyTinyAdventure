


using UnityEngine;

[RequireComponent(typeof(PoolBehaviour))]
public class LevelTagFactory :  Singleton<LevelTagFactory> {
    public PoolBehaviour Pool;
    protected override void Awake() {
        base.Awake();
        Pool = GetComponent<PoolBehaviour>();
    }

}