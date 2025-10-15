using UnityEngine;

[CreateAssetMenu(fileName = "ConsumableData", menuName = "Items/Items/ConsumableData", order = 1)]
public class ConsumableSO : ItemSO {
    public float CooldownDuration;
    public BuffSO Buff;
    public AudioClip ConsumeAudio;
}