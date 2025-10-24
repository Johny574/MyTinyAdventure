using UnityEngine;
using UnityEngine.UIElements;

public class JournalPanelController : PanelController {
    VisualTreeAsset _slot_t;

    public JournalPanelController(VisualTreeAsset panel_t, VisualElement root, bool dragable, AudioSource openaudio, AudioSource closeaudio) : base(panel_t, root, dragable, openaudio, closeaudio) {

    }

    public override void Setup() {
        // var journal = _player.GetComponent<QuestingBehaviour>().Questing;
        // ListView listView = _panel.Q<ListView>("Slots-view");
        // listView.makeItem = () => _slot_t.CloneTree();
        // listView.itemsSource = journal.ActiveQuests;
        // listView.bindItem = (element, index) => element.dataSource = journal.ActiveQuests[index];
    }
}