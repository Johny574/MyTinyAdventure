using System.Threading.Tasks;
using UnityEngine;

public class OrbSkill : ISkill {
    
    GameObject orb;
    private OrbSkillSO _skillData;

    public OrbSkill(SkillSO skillData) {
        _skillData = skillData as OrbSkillSO;
    }

    // public override SkillData Data { get => _skillData; set => _skillData = value as OrbSkillData; }



    public void OnCast(GameObject caster, Vector2 direction) {
        Spawn(caster, direction);
    }

    void Spawn(GameObject caster, Vector2 direction) {
        GameObject orb = Object.Instantiate(_skillData.OrbPrefab);
        orb.transform.position = (Vector2) caster.transform.position + (direction * _skillData._distance);
    }

    public void OnFinish(GameObject caster) {
        Object.Destroy(orb);
        orb = null;
    }

    public void OnTick(GameObject caster) {
        
    }


}