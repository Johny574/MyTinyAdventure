using FletcherLibraries;
using UnityEngine;

[RequireComponent(typeof(PoolBehaviour))]
public class PointerFactory: Singleton<PointerFactory> {
    public PoolBehaviour Pool;
    protected override void Awake() {
        base.Awake();
        Pool = GetComponent<PoolBehaviour>();
    }

}