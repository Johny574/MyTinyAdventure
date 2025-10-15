
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class FeedbackFactory: Factory<FeedbackFactory, int>
{
    public async Task Display(FeedbackData feedbackdata, Vector2 origin) {
        IPoolObject<FeedbackData> feedback = await Pool.GetObject<FeedbackData>();
        feedback.Bind(feedbackdata);
        float distance = 1f;
        Vector2 start = origin;
        Vector2 finish = (Vector2)origin + (Random.insideUnitCircle * distance);
         DOTween.To(() => start, x =>
        {
            start = x;
            ((MonoBehaviour)feedback).transform.position = start;
        }, finish, 1f).OnComplete(() => {
            ((MonoBehaviour)feedback).gameObject.SetActive(false);
        });
    }

}