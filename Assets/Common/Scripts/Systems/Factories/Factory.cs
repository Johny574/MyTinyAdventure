using System.Threading.Tasks;
using UnityEngine;


[RequireComponent(typeof(PoolBehaviour))]
public class Factory<M, T> : Singleton<M> where M : MonoBehaviour
{
    public PoolBehaviour Pool;
    protected override void Awake()
    {
        base.Awake();
        Pool = GetComponent<PoolBehaviour>();
    }

    public async Task<IPoolObject<T>> GetObject(T variant)
    {
        IPoolObject<T> obj = await Pool.GetObject<T>();
        obj.Bind(variant);
        return obj;
    }
}