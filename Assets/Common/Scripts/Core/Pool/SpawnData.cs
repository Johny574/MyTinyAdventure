using System;
using UnityEngine;

[Serializable]
public struct SpawnData {
    public Vector3 Position;
    public Vector3 Rotation;
    public Vector3 Scale;
    public bool Active;
    public Transform Parent;

    public SpawnData(Vector3 position, Vector3 rotation, Vector3 scale, bool active, Transform parent) {
        Position = position;
        Rotation = rotation;
        Scale = scale;
        Active = active;
        Parent = parent;
    }

    public void SetTransform(Transform obj) {
        if (Parent != null) { obj.transform.SetParent(Parent); }
        obj.transform.position = new Vector3(Position.x, Position.y, 0f);
        obj.transform.rotation = Quaternion.Euler(Rotation);
        obj.transform.localScale = Scale;
        obj.gameObject.SetActive(Active);
    }

    public static SpawnData FromTransform(Transform transform) => new SpawnData
    (
        transform.position,
        transform.rotation.eulerAngles,
        transform.localScale,
        transform.gameObject.activeSelf,
        transform.parent
    );
}
