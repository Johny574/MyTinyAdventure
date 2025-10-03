using System;
using System.Collections.Generic;
using FletcherLibraries;

[Serializable]
public class ResourceManager : Singleton<ResourceManager> {
    public Dictionary<Type, Array> Resources;
    protected override void Awake() {
        base.Awake();
        Resources = new() { { typeof(Recipe),           UnityEngine.Resources.LoadAll<Recipe>("Recipe") },
        };
    }

    public Array GetResource<T>() {
        if (!Resources.ContainsKey(typeof(T)))
            throw new Exception("Resource not found");

        return Resources[typeof(T)];
    }
}