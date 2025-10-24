using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class PanelController
{
    VisualTreeAsset _panel_t;
    protected VisualElement _panel;
    protected VisualElement _root;
    bool _dragable;
    public bool Enabled { get; private set; } = false;
    protected AudioSource _openAudio, _closeAudio;

    protected PanelController(VisualTreeAsset panel_t, VisualElement root, bool dragable, AudioSource openaudio, AudioSource closeaudio) {
        _panel_t = panel_t;
        _root = root;
        _dragable = dragable;
        _closeAudio = closeaudio;
        _openAudio = openaudio;
    }

    public void Awake() {
        var templateContainer = _panel_t.CloneTree();
        _panel = templateContainer.Children().First();
        _panel.RegisterCallback<PointerEnterEvent>((evt) => Player.Instance.Weapons.CanAttack = false);
        _panel.RegisterCallback<PointerMoveEvent>((evt) => Player.Instance.Weapons.CanAttack = false);
        _panel.RegisterCallback<PointerLeaveEvent>((evt) => Player.Instance.Weapons.CanAttack = true);
        var display = _root.Q<VisualElement>("HUD-container");
        display.Add(_panel);

        Button closeButton = _panel.Q<Button>("Close");
        closeButton.clicked += () => Disable();

        _panel.style.visibility = Visibility.Hidden;

        if (_dragable)
            _panel.AddManipulator(new PanelDragManipulator());
    }

    public abstract void Setup();

    public void Toggle() {
        if (Enabled) {
            Disable();
            return;
        }
        
        Open();
    }

    public virtual void Enable() {
        if (Enabled)
            return;

        Vector3 scale = Vector3.zero;
        _panel.style.scale = new StyleScale(new Scale(scale));
        _panel.style.visibility = Visibility.Visible;
        Vector3 targetScale = new Vector3(1f, 1f, 1f);
        _openAudio?.Play();

        DOTween.To(() => scale, x => {
            scale = x;
            _panel.style.scale = new StyleScale(new Scale(x));
        }, targetScale, 1f).SetEase(Ease.OutBounce);

        Enabled = true;
    }

    public void Disable()
    {
        if (!Enabled)
            return;

        Vector3 scale = new Vector3(1f, 1f, 1f);
        _panel.style.scale = new StyleScale(new Scale(scale));
        Vector3 targetScale = Vector3.zero;
        _closeAudio?.Play();

        DOTween.To(() => scale, x =>
        {
            scale = x;
            _panel.style.scale = new StyleScale(new Scale(x));
        }, targetScale, .5f).SetEase(Ease.Linear).OnComplete(PanelClose);

        Enabled = false;
    }

    void PanelClose()
    {
        _panel.style.visibility = Visibility.Hidden;
        OnPanelClosed();
    }

    protected virtual void OnPanelClosed() { }

  
    public virtual void Open()
    {
        Enable();
    }

    protected VisualElement CreateGhostIcon(VisualTreeAsset ghostIconTemplate) {
        VisualElement ghostIcon = ghostIconTemplate.CloneTree().Children().First();
        ghostIcon.style.width = new Length(30);
        ghostIcon.name = "GhostIcon";
        ghostIcon.style.height = new Length(30);
        ghostIcon.style.position = Position.Absolute;
        // _panel.parent.Add(ghostIcon);
        _root.parent.Add(ghostIcon);
        ghostIcon.style.visibility = Visibility.Hidden;
        return ghostIcon;
    }
}