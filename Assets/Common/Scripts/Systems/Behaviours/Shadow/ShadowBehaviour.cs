


using UnityEngine;

public class ShadowBehaviour : MonoBehaviour
{
    ShadowComponent _shadow;
    [SerializeField] private Transform _shadowObj;

    void Awake() {
        _shadow = new(this, _shadowObj);
    }
}