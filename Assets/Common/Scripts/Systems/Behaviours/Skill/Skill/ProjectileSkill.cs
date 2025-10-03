using UnityEngine;

public class ProjectileSkill : ISkill {
    private ProjectileSkillSO _skillData;

    public ProjectileSkill(SkillSO skillData) {
        _skillData = skillData as ProjectileSkillSO;
    }

    // public override SkillData Data { get => _skillData; set => _skillData = value as ProjectileSkillData; }

    // public ProjectileSkill(GameObject caster, SkillData skilldata) : base(caster, skilldata) {
    // }

    public void OnCast(GameObject caster, Vector2 direction) {
        // new ProjectileCommand.LaunchCommand(caster.GetComponent<EntityServiceBehaviour>().Service, caster.transform.position, direction, _skillData.Variant.GetComponent<Projectile>(), LayerMask.NameToLayer("PProjectile")).Execute();
    }

    public void OnFinish(GameObject caster) {
    }

    public void OnTick(GameObject caster) {
    }
}