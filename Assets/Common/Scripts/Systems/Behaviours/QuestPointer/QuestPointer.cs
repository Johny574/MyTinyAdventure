using UnityEngine;

public class QuestPointer : MonoBehaviour, IPoolObject<PointerData> {
    PointerData pointerdata;
    public void Bind(PointerData variant) {
        pointerdata = variant ;
    }
    void Update() {
        Point();
    }

    public void Point() {
        var delta = pointerdata.point - (Vector2)transform.position;
        transform.rotation = Quaternion.Euler(0, 0, Rotation2D.LookAngle(delta));
        transform.position = Rotation2D.ClampPointToCircle(pointerdata.point, pointerdata.pivot.transform.position, 1f);
    }
}

public class PointerData {

    public GameObject pivot;
    public Vector2 point;

    public PointerData(GameObject pivot, Vector2 point) {
        this.pivot = pivot;
        this.point = point;
    }
}