



using UnityEngine;

public class AimBehaviour : MonoBehaviour {
    public AimComponent Aim { get; private set; }
    [SerializeField] Transform _visionLight;
    
    void Awake() {
        Aim = new(this, _visionLight);
    }

    void Start() {
        Aim.Initilize(GetComponent<FlipBehaviour>().Flip);
    }

    void Update() {
        Aim.Update();        
    }
}