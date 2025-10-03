using System;
using System.Collections.Generic;
using FletcherLibraries;
using UnityEngine;

public class SceneTracker : Singleton<SceneTracker>
{
    public Dictionary<Type, List<GameObject>> Objects = new();

    protected override void Awake() {
        base.Awake();
        Objects = new() { { typeof(Enemy),  new() }, { typeof(NPC),  new() }, { typeof(ItemBehaviour),  new() }, { typeof(Coin),  new() }, { typeof(Container),  new() }, { typeof(Resource),  new() }, { typeof(ResourceSource),  new() }, { typeof(TravelPoint),  new() },
        };  
    }

     public GameObject GetClosestObject(List<GameObject> objects, Vector2 origin) {
        float closest = (origin - (Vector2)objects[0].transform.position).sqrMagnitude;
        float distance;
        int closestIndex = 0;

        for (int i = 0; i < objects.Count; i++) {
            distance = Vector2.Distance(origin, objects[i].transform.position);
            if (distance < closest) {
                closest = distance;
                closestIndex = i;
            }
        }
        return objects[closestIndex];
    }
    public void Register<T>(GameObject obj) {
        if (!Objects.ContainsKey(typeof(T)))
            Objects.Add(typeof(T), new List<GameObject>());

        Objects[typeof(T)].Add(obj);
    }

    public void Unregister<T>(GameObject obj) {
        Objects[typeof(T)].Remove(obj);
        if (Objects[typeof(T)].Count <= 0)
            Objects.Remove(typeof(T));
    }
}