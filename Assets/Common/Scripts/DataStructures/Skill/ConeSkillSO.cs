using UnityEngine;

[CreateAssetMenu(fileName = "Cone", menuName = "Skills/Cone", order = 1)]
public class ConeSkillSO : SkillSO {
    public float radius = 90f, _rayReach = 5f, _coneDistance = 5f;
    public LayerMask _enemyLayer;
    public AnimatorOverrideController cone;
}