using UnityEngine;

public class BuffsBehaviour : MonoBehaviour
{
    public BuffComponent Buffs { get; set; }

    void Awake() {
        Buffs = new(this);
    }

    void Start() {
        Buffs.Initilize();
    }
    
    void Update() {
        Buffs.Tick();
    }
}