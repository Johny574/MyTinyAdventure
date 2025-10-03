using System.Threading.Tasks;
using UnityEngine;

public class ConeSkill : ISkill {
    private ConeSkillSO _skillData;
    // public override SkillData Data { get => _skillData; set => _skillData = value as ConeSkillData; }

    private float _ray;
    int rayCount = 5;
    private GameObject _coneObj = null;

    public ConeSkill(SkillSO skillData) {
        _skillData = skillData as ConeSkillSO;
    }

    public void OnCast(GameObject caster, Vector2 direction) {
        // CreateConeAsync();
    }

    // async void CreateConeAsync() {
    //     Task<GameObject> poolObj = CentralManager.Instance.Manager<ObjectManager>().Pooler(Pooler.Type.Object, "FX").GetObject();
    //     await poolObj;
    //     _coneObj = poolObj.Result;
    //     _coneObj.GetComponent<Animator>().runtimeAnimatorController = _skillData.cone;
    //     _coneObj.GetComponent<Animator>().SetBool("Open", true);
    // }

    public void OnTick(GameObject caster) {
        // todo : integrate into a aim service.
        // EntityServiceBehaviour player = caster.GetComponent<EntityServiceBehaviour>();
        // var lookangle = Rotation2D.LookAngle(player.Service.Service<AimService>().Get<Vector2>(), 0);
        // if (lookangle < 0) _ = 360;


        // Vector3 leftbound = Quaternion.Euler(0, 0, _skillData.radius / 2) * player.Service.Service<AimService>().Get<Vector2>() * _skillData._rayReach;
        // Vector3 rightbound = Quaternion.Euler(0, 0, -_skillData.radius / 2) * player.Service.Service<AimService>().Get<Vector2>() * _skillData._rayReach;


        // Debug.DrawRay(caster.transform.position, leftbound, Color.green);
        // Debug.DrawRay(caster.transform.position, rightbound, Color.green);


        // for (int i = 0; i < rayCount; i++) {
        //     _ray = (_skillData.radius / 2) - (_skillData.radius * i / rayCount);
        //     Vector3 rayDirection = Quaternion.Euler(0, 0, _ray) * player.Service.Service<AimService>().Get<Vector2>() * _skillData._rayReach;
        //     RaycastHit2D hit = Physics2D.Raycast(caster.transform.position, rayDirection, _skillData._rayReach, _skillData._enemyLayer);
        //     Debug.DrawRay(caster.transform.position, rayDirection, Color.red);

        //     if (hit) {
        //         EnemyStateMachine enemy = hit.collider.gameObject.GetComponent<EnemyStateMachine>();
        //         (enemy.States["Damage"] as EnemyDamageState).Source = player.Service.Service<WeaponService>();
        //         if (enemy.CurrentState != "Damage") {
        //             enemy.ChangeState("Damage");
        //         }
        //     }
        // }
        // if (_coneObj != null) {
        //     _coneObj.transform.rotation = Quaternion.Euler(0, 0, Rotation2D.LookAngle(player.Service.Service<AimService>().Get<Vector2>(), 90));
        //     _coneObj.transform.position = (Vector2)player.transform.position + player.Service.Service<AimService>().Get<Vector2>() * _skillData._coneDistance;
        // }

    }

    public void OnFinish(GameObject caster) {
        throw new System.NotImplementedException();
    }

    // public override void OnFinish(EntityService caster) {
    //     _coneObj.SetActive(false);
    //     _coneObj = null;
    // }
}