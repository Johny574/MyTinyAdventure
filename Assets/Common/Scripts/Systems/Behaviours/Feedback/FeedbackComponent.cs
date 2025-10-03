


using System.Threading.Tasks;
using UnityEngine;

public class FeedbackComponent {
    public async Task DisplayFeedback(FeedbackData feedbackdata, Vector2 origin) {
        IPoolObject<FeedbackData> feedback = await FeedbackFactory.Instance.Pool.GetObject<FeedbackData>();
        feedback.Bind(feedbackdata);
        //todo fix this
        // ((MonoBehaviour)feedback).StartCoroutine(Transform2D.TransformTween(((MonoBehaviour)feedback).transform, origin, origin + UnityEngine.Random.insideUnitCircle * 2f, 5f, true, 1f));
    }
}