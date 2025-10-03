using System;
using UnityEngine;

public class CacheComponent : Component
{
    public GameObject CachedEntity { get; private set; }
    Sprite _cacheEmote;
    public Action<GameObject> Changed;
    EmoteComponent _emote;
    
    public CacheComponent(CacheBehaviour behaviour, Sprite cacheEmote) : base(behaviour) {
        _cacheEmote = cacheEmote;
    }

    public void Initilize(EmoteComponent emote) {
        _emote = emote;
    }  

    public void Add(GameObject entity) {
        CachedEntity = entity;
        Changed?.Invoke(CachedEntity);
        _emote.Add(_cacheEmote);
        CachedEntity.GetComponent<HealthBehaviour>().Health.Death += Remove;
    }

    public void Remove() {
        CachedEntity = null;
        Changed?.Invoke(CachedEntity);
    }
}