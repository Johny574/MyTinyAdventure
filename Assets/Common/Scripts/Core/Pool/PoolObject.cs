using System;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    public Action<GameObject> Disable;
    void OnDisable() => Disable?.Invoke(gameObject);
}