
using UnityEngine;

[CreateAssetMenu(fileName = "_buff", menuName = "Buffs/Damage", order = 1)]
public class DamageBuffData : BuffSO {
    public float Damage;
    public float DPS;
    public void SetDPS() => DPS = Damage / Duration;
}