using UnityEngine;

public class TimeSkill : ISkill {
    private TimeSkillData _skillData;

    public TimeSkill(SkillSO skillData) {
        _skillData = skillData as TimeSkillData;
    }

    // public override SkillData Data { get => _skillData; set => _skillData = value as TimeSkillData; }

    public void OnCast(GameObject caster, Vector2 direction) {
        TimeManager.Instance.Scale(_skillData.Scale);
    }

    public void OnFinish(GameObject caster) {
        TimeManager.Instance.Scale(1f);
    }

    public void OnTick(GameObject caster) {
    }
}