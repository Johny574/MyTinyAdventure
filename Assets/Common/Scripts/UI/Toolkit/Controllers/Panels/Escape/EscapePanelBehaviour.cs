


using System;
using FletcherLibraries;
using UnityEngine;
using UnityEngine.UIElements;

public class EscapePanelBehaviour : Singleton<EscapePanelBehaviour>
{
    [SerializeField] UIDocument _document;
    [SerializeField] VisualTreeAsset _panel_t;
    [SerializeField] bool _dragable = true;
    public PanelController Panel;
    [SerializeField] protected AudioSource _openAudio, _closeAudio;

    protected override void Awake() {
        base.Awake();
        Panel = new EscapePanelController(_panel_t, _document.rootVisualElement, _dragable, _closeAudio, _openAudio);
        Panel.Awake();
    }

    void Start() {
        Panel.Setup();
        InputManager.Instance.InputMappings["Escape"].Action.action.performed += ToggleEscapeMenu;
    }

    void OnDisable() {
        InputManager.Instance.InputMappings["Escape"].Action.action.performed -= ToggleEscapeMenu;
    }

    void ToggleEscapeMenu(UnityEngine.InputSystem.InputAction.CallbackContext context) => Panel.Toggle();

}