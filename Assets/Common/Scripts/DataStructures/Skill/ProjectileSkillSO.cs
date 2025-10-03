using UnityEngine;


[CreateAssetMenu(fileName = "Projectile", menuName = "Skills/Projectile", order = 1)]
public class ProjectileSkillSO : SkillSO {
    public GameObject Variant;
    // public override void Cast(EntityService caster, Vector2 direction)
    // {
    //     new ProjectileCommand.LaunchCommand(caster, caster.Component<Transform>().position, direction, _projectile, LayerMask.NameToLayer("PProjectile")).Execute();
    // }

    // public override void Finish(EntityService caster)
    // {
    // }

    // public override void Tick(EntityServiceBehaviour caster)
    // {
    // }
}