using System;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class SlotPanelController : PanelController
{
    protected VisualTreeAsset _ghostIcon_t, _stats_t;
    protected VisualElement _ghostIcon;
    protected ScrollView _scrollView;
    protected VisualElement _gridView;
    // VisualElement _tooltip; 
    public SlotPanelController(VisualTreeAsset panel_t, VisualTreeAsset tooltip_t, VisualElement root, bool dragable, AudioSource openaudio, AudioSource closeaudio, VisualTreeAsset ghostIcon_t, VisualTreeAsset stats_t) : base(panel_t, root, dragable, openaudio, closeaudio) {
        _ghostIcon_t = ghostIcon_t;
        _stats_t = stats_t;
        // _tooltip = tooltip_t.CloneTree();
        // _tooltip.style.visibility = Visibility.Hidden;
    }

    public void Initilize() {
        _ghostIcon = CreateGhostIcon(_ghostIcon_t);
    
        _scrollView = _panel.Q<ScrollView>("ScrollView");
        _gridView = new VisualElement();
        _gridView.AddToClassList("gridview");
        _gridView.AddToClassList("row");
        _scrollView.Add(_gridView);

        Setup();
        // _panel.Add(_tooltip);
    }

    public void Refresh(Array data) {
        Clear();
        Create(data);
        Draw();
        Update(data);
    }

    public abstract void Create(Array data);
    protected void Draw() {
        foreach (var slot in _gridView.Children()) {
            ScaleManipulator slotScaleManipulator = new ScaleManipulator(new Vector2(1.2f, 1.2f));
            slot.AddManipulator(slotScaleManipulator);
            slot.RegisterCallback<MouseEnterEvent>(evt => {
                Vector2 mousePos = evt.mousePosition + new Vector2(1f, 1f);
                // TooltipBehaviour.Instance.Show(mousePos,);
            });
            slot.RegisterCallback<MouseLeaveEvent>(evt => {
                // _tooltip.style.visibility = Visibility.Hidden;
                // _tooltip.Hide();
            });
            slot.style.alignSelf = new StyleEnum<Align>(Align.FlexStart);
        }
    }
    
    protected void Clear() => _gridView.hierarchy.Clear();
    protected abstract void Update(Array data);
}
