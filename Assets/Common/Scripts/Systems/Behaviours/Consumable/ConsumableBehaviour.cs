
using UnityEngine;

public class ConsumableBehaviour : MonoBehaviour
{
    public ConsumableComponent Consumables { get; set; }
    void Awake() {
        Consumables = new(this);
    }
    
    void Update() {
        Consumables.Update();
    }
}