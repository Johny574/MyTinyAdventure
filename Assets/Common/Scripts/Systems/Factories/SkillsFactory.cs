using System;
using System.Collections.Generic;
using UnityEngine;

public static class SkillsFactory  {
    public static Dictionary<Type, Func<SkillSO, GameObject, ISkill>> Skills = new() { { typeof(BuffSkillSO),            (data, user) => new BuffSkill(data)},
		{ typeof(ConeSkillSO),            (data, user) => new ConeSkill(data)},
		{ typeof(TimeSkillData),            (data, user) => new TimeSkill(data)},
		{ typeof(OrbSkillSO),             (data, user) => new OrbSkill(data)},
		{ typeof(ProjectileSkillSO),      (data, user) => new ProjectileSkill(data)},
		{ typeof(BlinkSkillSO),           (data, user) => new BlinkSkill(data)},
    };
}