using UnityEngine;

[CreateAssetMenu(fileName = "Blink", menuName = "Skills/Blink", order = 1)]
public class BlinkSkillSO : SkillSO {
    public float Distance = 5f;
    public LayerMask WallLayer;
}