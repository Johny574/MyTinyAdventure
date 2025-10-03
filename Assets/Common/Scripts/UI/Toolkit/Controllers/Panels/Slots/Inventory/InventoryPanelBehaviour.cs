using System;
using FletcherLibraries;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryPanelBehaviour : Singleton<InventoryPanelBehaviour>
{
    [SerializeField] UIDocument _document;
    [SerializeField] VisualTreeAsset _panel_t;
    [SerializeField] bool _dragable = true;
    [SerializeField] VisualTreeAsset _ghostIcon_t, _stats_t;
    public InventoryPanelController Panel;
    [SerializeField] protected AudioSource _openAudio, _closeAudio;

    protected override void Awake() {
        base.Awake();
        Panel = new InventoryPanelController(_panel_t, _document.rootVisualElement, _dragable, _closeAudio, _openAudio, _ghostIcon_t, _stats_t);
        Panel.Awake();
    }
    void Start() {
        Panel.Initilize();
    }

    void OnEnable() {
        InputManager.Instance.InputMappings["Inventory"].Action.action.performed += InventoryToggle;
        ContainerEvents.Instance.Open += ContainerOpen;
        ShopEvents.Instance.Open += ShopOpen;
    }

    void OnDisable() {
        InputManager.Instance.InputMappings["Inventory"].Action.action.performed -= InventoryToggle;
        ShopEvents.Instance.Open -= ShopOpen;
        ContainerEvents.Instance.Open -= ContainerOpen;
    }

    void InventoryToggle(UnityEngine.InputSystem.InputAction.CallbackContext context) => Panel.Toggle();
    void ShopOpen(ShopComponent component, GameObject @object) => Panel.Open();
    void ContainerOpen(Container container) => Panel.Open();
}