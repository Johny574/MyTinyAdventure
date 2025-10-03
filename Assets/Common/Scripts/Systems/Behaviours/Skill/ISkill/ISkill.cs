using UnityEngine;

public interface ISkill {
    public abstract void OnFinish(GameObject caster);
    public abstract void OnCast(GameObject caster, Vector2 direction);
    public abstract void OnTick(GameObject caster);
}