using UnityEngine;

public interface IDamage {
    public abstract float Damage();
    public abstract GameObject Source();
    public abstract void Hit(Collider2D col);
}