using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SceneTracker : Singleton<SceneTracker>
{
    public Dictionary<Type, List<GameObject>> Objects = new();
    public Dictionary<Type, Dictionary<int, GameObject>> UniqueObjects = new();

    protected override void Awake() {
        base.Awake();
        Objects = new();
        UniqueObjects = new();
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

    public void RegisterUnique<T>(GameObject obj, int UID) {
        if (!UniqueObjects.ContainsKey(typeof(T)))
            UniqueObjects.Add(typeof(T), new());

        if (UniqueObjects[typeof(T)].ContainsKey(UID))
            return;

        UniqueObjects[typeof(T)].Add(UID, obj);
        Register<T>(obj);
    }

    public void UnregisterUnique<T>(int UID)
    {
        if (!UniqueObjects.ContainsKey(typeof(T)))
            return;

        GameObject GameObject = UniqueObjects[typeof(T)][UID];
        UniqueObjects[typeof(T)].Remove(UID);
        Unregister<T>(GameObject);
    }

    public GameObject GetUnique(int UID) => UniqueObjects.SelectMany(x => x.Value).Where(x => x.Key == UID).First().Value;

    public void Register<T>(GameObject obj)
    {
        if (!Objects.ContainsKey(typeof(T)))
            Objects.Add(typeof(T), new List<GameObject>());

        Objects[typeof(T)].Add(obj);
    }

    public void Unregister<T>(GameObject obj)
    {
        if (!Objects.ContainsKey(typeof(T)))
            return;

        Objects[typeof(T)].Remove(obj);

        if (Objects[typeof(T)].Count <= 0)
            Objects.Remove(typeof(T));
    }

    public void Clear()
    {
        Objects.Clear();
        UniqueObjects.Clear();   
    }
}