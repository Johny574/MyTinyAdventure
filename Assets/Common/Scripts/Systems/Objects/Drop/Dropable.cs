
using DG.Tweening;
using UnityEngine;

public class Dropable : MonoBehaviour {
    public void Drop(Vector2 origin)
    {
        transform.position = origin;
        gameObject.SetActive(true);

        float distance = 1f;
        Vector2 start = origin;
        Vector2 finish = (Vector2)origin + (Random.insideUnitCircle * distance);

        DOTween.To(() => start, x =>
        {
            start = x;
            gameObject.transform.position = start;
        }, finish, 1f);
    }
}