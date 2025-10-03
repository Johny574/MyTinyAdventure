using System;
using UnityEngine;

[Serializable]
public class Component
{
    public MonoBehaviour Behaviour;

    public Component(MonoBehaviour behaviour) {
        Behaviour = behaviour;
    }
}
