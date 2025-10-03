


using UnityEngine;


[RequireComponent(typeof(StatpointsBehaviour))]
public class ManaBehaviour : MonoBehaviour
{
    public ManaComponent Mana;
    void Awake() {
        Mana = new(this);
    }

    void Start() {
        Mana.Initilize();
    }

    void Update() {
        Mana.Tick();
    }
}