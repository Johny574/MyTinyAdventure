



using System;
using FletcherLibraries;
using UnityEngine;
using UnityEngine.UIElements;

public class CraftingPanelBehaviour : Singleton<CraftingPanelBehaviour>
{
    [SerializeField] UIDocument _document;
    [SerializeField] VisualTreeAsset _panel_t;
    [SerializeField] bool _dragable = true;
    [SerializeField] VisualTreeAsset _slot_t;
    public PanelController Panel;
    [SerializeField] protected AudioSource _openAudio, _closeAudio;

    protected override void Awake() {
        base.Awake();
        Panel = new CraftingPanelController(_panel_t, _document.rootVisualElement, _dragable, _closeAudio, _openAudio, _slot_t);
        Panel.Awake();
    }
    void Start() {
        Panel.Setup();
        InputManager.Instance.InputMappings["Crafting"].Action.action.performed += ToggleCraftingPanel;
    }
    void OnDisable() {
        InputManager.Instance.InputMappings["Crafting"].Action.action.performed -= ToggleCraftingPanel;
    }

    private void ToggleCraftingPanel(UnityEngine.InputSystem.InputAction.CallbackContext context) => Panel.Toggle();
}