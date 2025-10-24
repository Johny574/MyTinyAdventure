using System.Threading.Tasks;
using UnityEngine;

public class TagFactory : Factory<TagFactory, string> {
      public async Task<IPoolObject<string>> CreateTag(string data, GameObject follow) {
        IPoolObject<string> obj = await GetObject(data);
        ((MonoBehaviour)obj).GetComponent<Follower>().Follow(follow);
        // health.Changed += (data, amount) => obj.Bind(data);
        return obj;
    }
    
}