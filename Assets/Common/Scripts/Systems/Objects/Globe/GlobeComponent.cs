using System;
using UnityEngine;


[Serializable]
public abstract class GlobeComponent : Component, ICollectable
{
    [SerializeField] protected int _amount;

    protected GlobeComponent(GlobeBehaviour behaviour) : base(behaviour) {
    }

    public abstract void Collect(GameObject collector);
    
    public enum Type {
        Health,
        Experience
    }

    // void Update() {
    //     if (Vector2.Distance(transform.position, PlayerEntityService.Instance.Component<Transform>().position) < PickupRadius) {
    //         var dif = (PlayerEntityService.Instance.Component<Transform>().position - transform.position).normalized;
    //         transform.position += dif * FollowSpeed;
    //     }
    // }
    // [SerializeField] private Animator _renderer;

    // public virtual void Bind<T>(T variant) {
    // Globe o = ((GameObject)(object)variant).GetComponent<Globe>();
    // PickupRadius = o.PickupRadius;
    // FollowSpeed = o.FollowSpeed;
    // _renderer.runtimeAnimatorController = o.gameObject.GetComponent<Animator>().runtimeAnimatorController;
    // }


}