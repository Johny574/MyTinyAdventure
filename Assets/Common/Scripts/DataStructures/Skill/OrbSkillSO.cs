using UnityEngine;

[CreateAssetMenu(fileName = "Orb", menuName = "Skills/Orb", order = 1)]
public class OrbSkillSO : SkillSO {
    public float _duration, _distance;
    public BuffSO _debuff;
    [SerializeField] public GameObject OrbPrefab;

    // public override void Cast(EntityService caster, Vector2 direction)
    // {
    //     Spawn(caster, direction);
    // }

    // void Spawn(EntityService caster, Vector2 direction)
    // {
    //     // Task<DynamicBehaviour> poolObj = ObjectManager.Instance.Pooler(Pooler.Type.Object, "Orb").GetObject();
    //     // await poolObj;
    //     // poolObj.Result.transform.position = (Vector2)caster.Component<Transform>().position + (direction * _distance);
        
    //     GameObject orb = GameObject.Instantiate(_orbPrefab);
    //     orb.transform.position = (Vector2) caster.Component<Transform>().position + (direction * _distance);
    // }

    // public override void Tick(EntityServiceBehaviour caster)
    // {

    // }

    // public override void Finish(EntityService caster)
    // {
    //     GameObject.Destroy(orb);
    //     orb = null;
    //     Finish(caster);
    // }
}