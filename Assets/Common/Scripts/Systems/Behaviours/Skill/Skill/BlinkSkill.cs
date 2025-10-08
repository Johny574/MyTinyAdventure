using UnityEngine;

public class BlinkSkill : ISkill {
    private BlinkSkillSO _skillData;
    float _trailtime;
    bool _emitting;
    GameObject _caster;
    TrailRenderer _trial;

    public BlinkSkill(SkillSO skillData)//GameObject caster)
    {
        _skillData = skillData as BlinkSkillSO;
        // _caster = caster;
        // _trial = _caster.GetComponent<TrailRenderer>();
    }

    public void OnCast(GameObject caster, Vector2 direction) => Cast(caster, direction);

    public void OnTick(GameObject caster) {
        if (!_emitting)
            return;

        if (_trailtime < .5f) {
            _trailtime += Time.deltaTime;
        }
        else {
            _emitting = false;
            _trailtime = 0f;
            // caster.GetComponent<TrailRenderer>().emitting = false;
        }
    }

    public void OnFinish(GameObject caster) {
    }

    void Cast(GameObject caster, Vector2 direction) {
        RaycastHit2D hit = Physics2D.Raycast(caster.transform.position, direction, _skillData.Distance, _skillData.WallLayer);

        if (hit) {
            caster.transform.position = hit.point;
            return;
        }

        // _trial.emitting = true;
        caster.transform.position = (Vector2)caster.transform.position + direction * _skillData.Distance;
        // _trial.emitting = false;
    }
}