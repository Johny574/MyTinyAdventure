using UnityEngine;

public class BuffSkill : ISkill {
    private BlinkSkillSO _skillData;

    public BuffSkill(SkillSO skillData) {
        _skillData = skillData as BlinkSkillSO;
    }

    //     public BuffData Buff;

    //     public BuffSkill(EntityService caster, SkillData skilldata) : base(caster, skilldata) {
    //     }

    //     private BuffSkillData _skillData;
    //     public override SkillData Data { get => _skillData; set => _skillData = value as BuffSkillData; }

    //     public override void OnCast(GameObject caster, Vector2 direction) {
    //     }

    //     public override void OnFinish(EntityService caster) {
    //     }

    //     public override void OnTick(GameObject caster) {
    //     }

    // public override SkillData Data { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void OnCast(GameObject caster, Vector2 direction) {
        throw new System.NotImplementedException();
    }

    public void OnFinish(GameObject caster) {
        throw new System.NotImplementedException();
    }

    public void OnTick(GameObject caster) {
        throw new System.NotImplementedException();
    }
}