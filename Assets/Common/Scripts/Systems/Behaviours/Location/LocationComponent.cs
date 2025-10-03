using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocationComponent : Component, ISerializedComponent<Vector2>
{
    public int CurrentScene = 1;

    public LocationComponent(LocationBehaviour behaviour) : base(behaviour) {
    }

    public void Initilize() {
        CurrentScene = Array.IndexOf(Locations.Scenes, SceneManager.GetActiveScene().name);
    }

    public void Load(Vector2 save) => Behaviour.transform.position = save;
    public Vector2 Save() => Behaviour.transform.position;

    // public override void Add<T>(T obj) {

    // }

    // public override T Get<T>() => (T)(object)new KeyValuePair<int, Counter>(CurrentScene, new Counter(x, y));

    // public override void Remove<T>(T obj) {

    // }

    // public override void Set<T>(T data) {
    //     KeyValuePair<int, Counter> d = (KeyValuePair<int, Counter>)(object)data;
    //     CurrentScene = d.Key;
    //     x = d.Value.Count;
    //     y = d.Value.Limit;
    //     Entity.Component<Transform>().position = new Vector2(x, y);
    // }

    // public virtual void Tick() {
    //     x = Entity.Component<Transform>().position.x;
    //     y = Entity.Component<Transform>().position.y;
    // }
}