using System.Threading.Tasks;
using UnityEngine;

public class HealthbarFactory : Factory<HealthbarFactory, BarData>
{
    public async Task<IPoolObject<BarData>> CreateHealthBar(BarData data, HealthComponent health) {
        IPoolObject<BarData> obj = await GetObject(data);
        ((MonoBehaviour)obj).GetComponent<Follower>().Follow(health.Behaviour.gameObject);
        health.Changed += obj.Bind;
        return obj;
    }
}