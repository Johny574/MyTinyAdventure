using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GearPanelBehaviour : Singleton<GearPanelBehaviour>
{
    [SerializeField] UIDocument _document;
    [SerializeField] VisualTreeAsset _panel_t, _ghosticon_t;
    [SerializeField] bool _dragable = true;
    GearPanelController _panel;
    [SerializeField] protected AudioSource _openAudio, _closeAudio;
    void Start() {
        _panel = new GearPanelController(_panel_t, _document.rootVisualElement, _dragable, _closeAudio, _openAudio, _ghosticon_t);
        _panel.Awake();
        _panel.Setup();
        InputManager.Instance.InputMappings["Inventory"].Action.action.performed += OpenGearPanel;
    }

    void OnDisable() {
        InputManager.Instance.InputMappings["Inventory"].Action.action.performed -= OpenGearPanel;
    }

    void OpenGearPanel(UnityEngine.InputSystem.InputAction.CallbackContext context) => _panel.Toggle();
}