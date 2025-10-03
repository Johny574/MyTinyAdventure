


using UnityEngine;

public class CacheBehaviour : MonoBehaviour
{
    public CacheComponent Cache { get; set; }
    [SerializeField] Sprite _cacheEmote; 

    void Awake() {
        Cache = new(this, _cacheEmote);
    }


    void Start() {
        Cache.Initilize(GetComponent<EmoteBehaviour>().Emotes);
    }
}