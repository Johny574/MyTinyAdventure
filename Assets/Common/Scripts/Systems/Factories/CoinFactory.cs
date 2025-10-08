using UnityEngine;

public class CoinFactory: Factory<CoinFactory, int>
{
    public async void Drop(Vector2 origin, int amount)
    {
        IPoolObject<int> obj = await GetObject(amount);
        ((MonoBehaviour)obj).GetComponent<Dropable>().Drop(origin);
    }
}