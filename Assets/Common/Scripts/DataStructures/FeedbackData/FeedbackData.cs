using UnityEngine;

public struct FeedbackData
{
    public string Text;
    public Color Color;

    public FeedbackData(string text, Color color) {
        Text = text;
        Color = color;
    }
}