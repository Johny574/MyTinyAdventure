




using System.Threading.Tasks;
using FletcherLibraries;
using UnityEngine;

[RequireComponent(typeof(PoolBehaviour))]
public class CoinFactory: Singleton<CoinFactory> {
    public PoolBehaviour Pool;
    protected override void Awake() {
        base.Awake();
        Pool = GetComponent<PoolBehaviour>();
    }

}