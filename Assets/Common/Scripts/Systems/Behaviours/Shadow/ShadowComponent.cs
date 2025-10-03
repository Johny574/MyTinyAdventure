using System;
using UnityEngine;

public class ShadowComponent : Component
{
    private Transform _shadow;

    public ShadowComponent(ShadowBehaviour behaviour, Transform shadow) : base(behaviour) {
        _shadow = shadow;
    }

    void Update() {
        // Vector3 dif = _behaviour.transform.position - GlobalLight2D.Instance.GlobalLightSource.transform.position;
        // float angle = Rotation2D.LookAngle(dif);
        // _shadow.transform.rotation = Quaternion.Euler(0f,0f, angle);
    }
}