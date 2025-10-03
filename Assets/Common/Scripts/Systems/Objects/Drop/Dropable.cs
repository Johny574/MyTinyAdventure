
using UnityEngine;

public class Dropable : MonoBehaviour {
    public void Drop(Vector2 origin) {
        transform.position = origin;
        gameObject.SetActive(true);
        // todo fix this
        // StartCoroutine(Transform2D.TransformTween(transform, transform.position, (Vector2)transform.position + Random.insideUnitCircle * 2f, 5f));
    }
}