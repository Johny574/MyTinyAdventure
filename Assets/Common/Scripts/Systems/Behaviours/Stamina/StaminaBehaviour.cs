


using UnityEngine;

[RequireComponent(typeof(StatpointsBehaviour))]
public class StaminaBehaviour : MonoBehaviour
{
    public StaminaComponent Stamina { get; set; }
    
    void Awake() {
        Stamina = new(this);
    }
    void Start() {
        Stamina.Initilize();
    }

    void Update() {
        Stamina.Tick();
    }
}