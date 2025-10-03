

using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Tag))]
public class Feedback : MonoBehaviour, IPoolObject<FeedbackData>
{
    Tag _tag;
    void Awake() {
        _tag = GetComponent<Tag>();
    }  

    public void Bind(FeedbackData variant) {
        _tag.Bind(variant.Text);
        _tag._textField.color = variant.Color;
    }
}