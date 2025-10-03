




using UnityEngine;
using UnityEngine.UIElements;

public class EscapePanelController : PanelController
{
    
    public EscapePanelController(VisualTreeAsset panel_t, VisualElement root, bool dragable, AudioSource closeaudio, AudioSource openaudio) : base(panel_t, root, dragable, closeaudio, openaudio) {
    }

    public override void Setup() {
        
    }
}