
using UnityEngine;

public class InteractBehaviour : MonoBehaviour
{
    public InteractComponent Interact { get; set; }

    void Awake() {
        Interact = new(this);
    }

    void Start() {
        Interact.Initilize();
    }

    void Update() {
        Interact.Update();
    }
    
    public void OnTriggerEnter2D(Collider2D col) {
        if (!LayerMask.LayerToName(col.gameObject.layer).Equals("Interact"))
            return;

        Interact.OnTriggerEnter2D(col);
    }

    public void OnTriggerExit2D(Collider2D col) {
        if (!LayerMask.LayerToName(col.gameObject.layer).Equals("Interact"))
            return;

        Interact.OnTriggerExit2D(col);
    }
}