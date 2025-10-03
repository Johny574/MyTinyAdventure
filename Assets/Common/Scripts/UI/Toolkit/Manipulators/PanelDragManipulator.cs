using UnityEngine;
using UnityEngine.UIElements;

public class PanelDragManipulator : PointerManipulator
{
    bool isDragging = false;
    Vector3 offset;

    public PanelDragManipulator() {
        activators.Add(new ManipulatorActivationFilter { button = MouseButton.LeftMouse });
    }

    protected override void RegisterCallbacksOnTarget() {
        target.RegisterCallback<PointerDownEvent>(OnPointerDown);
        target.RegisterCallback<PointerMoveEvent>(OnPointerMove);
        target.RegisterCallback<PointerUpEvent>(OnPointerUp);
    }

    protected override void UnregisterCallbacksFromTarget() {
        target.UnregisterCallback<PointerDownEvent>(OnPointerDown);
        target.UnregisterCallback<PointerMoveEvent>(OnPointerMove);
        target.UnregisterCallback<PointerUpEvent>(OnPointerUp);
    }

    void OnPointerMove(PointerMoveEvent evt) {
        if (!target.HasPointerCapture(evt.pointerId) || !isDragging) return;

        Vector3 dif = evt.localPosition - offset;
        target.transform.position += dif;
        evt.StopPropagation();
    }

    void OnPointerDown(PointerDownEvent evt) {
        if (!CanStartManipulation(evt) || isDragging) return;
        offset = evt.localPosition;
        isDragging = true;
        target.CapturePointer(evt.pointerId);
        target.BringToFront();
        evt.StopPropagation();
    }

    void OnPointerUp(PointerUpEvent evt) {
        if (!CanStopManipulation(evt) || !isDragging) return;
        isDragging = false;
        target.ReleasePointer(evt.pointerId);
        evt.StopPropagation();
    }
}