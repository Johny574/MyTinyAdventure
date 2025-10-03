



using System;
using FletcherLibraries;
using UnityEngine;
using UnityEngine.UIElements;

public class JournalPanelBehaviour : Singleton<JournalPanelBehaviour>
{
    [SerializeField] UIDocument _document;
    [SerializeField] VisualTreeAsset _panel_t;
    [SerializeField] bool _dragable = true;
    public PanelController Panel;
    [SerializeField] protected AudioSource _openAudio, _closeAudio;
    
     protected override void Awake() {
        base.Awake();
        Panel = new JournalPanelController(_panel_t,   _document.rootVisualElement, _dragable, _closeAudio, _openAudio);
        Panel.Awake();
    }

    void Start() {
        Panel.Setup();
        InputManager.Instance.InputMappings["Journal"].Action.action.performed += ToggleJournalPanel;
    }

    void OnDisable() {
        InputManager.Instance.InputMappings["Journal"].Action.action.performed -= ToggleJournalPanel;
    }

    void ToggleJournalPanel(UnityEngine.InputSystem.InputAction.CallbackContext context) => Panel.Toggle();
}