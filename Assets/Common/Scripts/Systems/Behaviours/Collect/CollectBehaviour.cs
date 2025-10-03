using UnityEngine;

public class CollectBehaviour : MonoBehaviour {
    CollectComponent _collect;
    void Awake() {
        _collect = new(this);
    }

    public void OnTriggerEnter2D(Collider2D col) {
        if (!LayerMask.LayerToName(col.gameObject.layer).Equals("Collectable"))
            return;

        _collect.OnTriggerEnter2D(col);
    }
}   