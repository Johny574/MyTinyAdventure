






using FletcherLibraries;
using UnityEngine;
using UnityEngine.UIElements;

public class ContainerPanelBehaviour : Singleton<ContainerPanelBehaviour>
{
    [SerializeField] UIDocument _document;

    [SerializeField] protected AudioSource _openAudio, _closeAudio;
    [SerializeField] VisualTreeAsset _panel_t;
    [SerializeField] bool _dragable = true;
    [SerializeField] VisualTreeAsset _slot_t, _ghostIcon_t, _stats_t;
    public ContainerPanelController Panel;

    protected override void Awake() {
        base.Awake();
        Panel = new ContainerPanelController(_panel_t, _document.rootVisualElement, _dragable, _openAudio, _closeAudio, _ghostIcon_t, _stats_t);
        Panel.Awake();
    }

    void Start() {
        Panel.Initilize();
    }
}