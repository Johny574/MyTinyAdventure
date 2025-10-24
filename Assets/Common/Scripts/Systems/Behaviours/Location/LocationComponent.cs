using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocationComponent : Component, ISerializedComponent<Vector2>
{
    public string CurrentScene;

    public LocationComponent(LocationBehaviour behaviour) : base(behaviour) {
    }

    public void Initilize() {
        CurrentScene = SceneManager.GetActiveScene().name;
    }

    public void Load(Vector2 save) => Behaviour.transform.position = save;
    public Vector2 Save() => Behaviour.transform.position;
}