using UnityEngine;
using UnityEngine.UIElements;

public class ScaleManipulator : PointerManipulator
{
    Vector2 _defaultScale, _hoverScale;

    public ScaleManipulator(Vector2 hoverScale) {
        _hoverScale = hoverScale;
        _defaultScale = Vector2.one;
    }

    protected override void RegisterCallbacksOnTarget() {
        target.RegisterCallback<PointerEnterEvent>(OnPointerEnter);
        target.RegisterCallback<PointerLeaveEvent>(OnPointerLeave);
    }

    private void OnPointerLeave(PointerLeaveEvent evt) {
        target.style.scale = new StyleScale(new Scale(_defaultScale));
    }

    private void OnPointerEnter(PointerEnterEvent evt) {
        target.style.scale = new StyleScale(new Scale(_hoverScale));
    }

    protected override void UnregisterCallbacksFromTarget() {
        target.UnregisterCallback<PointerEnterEvent>(OnPointerEnter);
        target.UnregisterCallback<PointerLeaveEvent>(OnPointerLeave);
    }
}