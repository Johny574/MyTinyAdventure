using FletcherLibraries;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopPanelBehaviour : Singleton<ShopPanelBehaviour>
{
    [SerializeField] VisualTreeAsset _slot_t, _ghost_t, _stats_t;
    [SerializeField] UIDocument _document;
    [SerializeField] VisualTreeAsset _panel_t;
    [SerializeField] bool _dragable = true;
    public ShopPanelController Panel;
    [SerializeField] protected AudioSource _openAudio, _closeAudio;

    protected override void Awake() {
        base.Awake();
        Panel = new ShopPanelController(_panel_t, _document.rootVisualElement, _dragable, _openAudio, _closeAudio, _ghost_t, _stats_t);
        Panel.Awake();
    }

    void Start() {
        Panel.Initilize();
        ShopEvents.Instance.Open += Panel.Open;
        ShopEvents.Instance.Close += Panel.Close;
    }


    void OnDisable() {
        ShopEvents.Instance.Open -= Panel.Open;
        ShopEvents.Instance.Close -= Panel.Close;
    }
}