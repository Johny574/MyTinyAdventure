using UnityEngine;
using UnityEngine.UIElements;

public class SkillsPanelBehaviour : Singleton<SkillsPanelBehaviour>
{
    [SerializeField] UIDocument _document;
    [SerializeField] VisualTreeAsset _panel_t;
    [SerializeField] bool _dragable = true;
    [SerializeField] VisualTreeAsset _slot_t, _ghostIcon_t;
    public PanelController Panel;
    [SerializeField] protected AudioSource _openAudio, _closeAudio;

    protected override void Awake() {
        base.Awake();
        Panel = new SkillsPanelController(_panel_t, _document.rootVisualElement, _dragable, _closeAudio, _openAudio, _slot_t, _ghostIcon_t);
        Panel.Awake();
    }
    void Start() {
        Panel.Setup();
        InputManager.Instance.InputMappings["Skills"].Action.action.performed += ToggleSkillsPanel;
    }

    void OnDisable() {
        InputManager.Instance.InputMappings["Skills"].Action.action.performed -= ToggleSkillsPanel;
    }

    private void ToggleSkillsPanel(UnityEngine.InputSystem.InputAction.CallbackContext context) => Panel.Toggle();
}