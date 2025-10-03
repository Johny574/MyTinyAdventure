

using System;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemStackPanelBehaviour : Singleton<ItemStackPanelBehaviour>
{
    [SerializeField] UIDocument _document;
    [SerializeField] VisualTreeAsset _panel_t;
    [SerializeField] bool _dragable = true;
    public ItemStackPanelController Panel { get; set; }
    [SerializeField] protected AudioSource _openAudio, _closeAudio;
    protected override void Awake() {
        base.Awake();
        Panel = new(_panel_t, _document.rootVisualElement, _dragable, _openAudio, _closeAudio);
        Panel.Awake();
    }

    void Start() {
        Panel.Setup();
    }

    public void Open(ItemStackCommand stack, Action confirmaction) {
        Panel.Open(stack, confirmaction);
    }
}