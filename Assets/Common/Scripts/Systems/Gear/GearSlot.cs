using System;
using UnityEngine;

[Serializable]
public class Gearslot {
    public GearItemSO Item;
    public GameObject Object;
    public Animator Animator;
    public SpriteRenderer Renderer;
    public Gearslot(GearItemSO item, GameObject @object, Animator animator) {
        Item = item;
        Object = @object;
        Animator = animator;
        Renderer = @object?.GetComponent<SpriteRenderer>();
    }
}