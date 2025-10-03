




using UnityEngine;
using UnityEngine.UIElements;

public abstract class UISlot : VisualElement 
{
    public VisualElement Icon;

    public UISlot() {
        Icon = new VisualElement();
        Icon.AddToClassList("slot-icon");
        Add(Icon);
    }

    public abstract void OnDrop<T>(T data);
    public abstract void Update<T>(T data);
}