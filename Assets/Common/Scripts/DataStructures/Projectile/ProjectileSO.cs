using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileSO", menuName = "Projectile", order = 1)]
public class ProjectileSO : ScriptableObject {
    public float TravelDuration = 5f;
    public float TravelSpeed = 5f;
    public float Lookoffset = -90;
    public AnimatorOverrideController Animation;
    public Sprite Sprite;
    public WeaponItemSO.Type Type;
}