using System;
using UnityEngine;

[Serializable]
public class Skill {
    public bool Cooldown { get; private set; }
    public SkillSO Data;
    public float Timer;
    public float Fill;
    private GameObject _caster;
    ISkill skill;

    public Skill(GameObject caster, SkillSO skilldata, float timer=0f) {
        _caster = caster;
        Data = skilldata;
        Timer = timer;
        Cooldown = false;
        skill = SkillsFactory.Skills[skilldata.GetType()].Invoke(skilldata, caster);
    }

    public void Cast(Vector2 direction) {
        if (Data == null) {
            return;
        }

        Cooldown = true;
        
        skill.OnCast(_caster, direction);
    }
    
    public void Tick() {
        if (!Cooldown) {
            return;
        }

        if (Timer < Data.CooldownDuration) {
            Timer += Time.deltaTime;
            skill.OnTick(_caster);
            Fill = 1 - Timer / Data.CooldownDuration ;
        }
        else {
            Timer = 0f;
            skill.OnFinish(_caster);
            Fill = Timer / Data.CooldownDuration ;
            Cooldown = false;
        }
    }
}