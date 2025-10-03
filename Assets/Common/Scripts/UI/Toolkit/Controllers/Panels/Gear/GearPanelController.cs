using UnityEngine;
using UnityEngine.UIElements;

public class GearPanelController : PanelController
{
    VisualElement _ghostIcon;
    VisualTreeAsset _ghostIcon_t;
    VisualElement _container;
    public GearPanelController(VisualTreeAsset panel_t, VisualElement root, bool dragable, AudioSource openaudio, AudioSource closeaudio, VisualTreeAsset ghosticon_t) : base(panel_t, root, dragable, closeaudio, openaudio) {
        _ghostIcon_t = ghosticon_t;
    }

    public override void Setup() {
        _ghostIcon = CreateGhostIcon(_ghostIcon_t);
        _container = _panel.Q<VisualElement>("GearSlots");
        CreateGearSlots(Player.Instance.Gear.Gear, Player.Instance.Stats.Stats);
        var xpbar = _panel.Q<VisualElement>("XP-bar");
        xpbar.dataSource = Player.Instance.Experience.Experience;
    }

    public override void Enable() {
        base.Enable();
        RefreshStatBars(Player.Instance.Stats.Stats.StatPoints);
    }

    void CreateGearSlots(GearComponent gear, StatpointsComponent statpoints) {
        gear.Updated += RefreshGear;
        statpoints.Changed += RefreshStatBars;

        foreach (var key in gear.Gear.Keys) {
            GearUISlot slot = _container.Q<GearUISlot>(key.ToString());
            DragAndDropManipulator dragAndDrop = new DragAndDropManipulator(_ghostIcon, () => gear.UnEquipt(key));
            slot.AddManipulator(dragAndDrop);
        }

        RefreshGear(gear.Gear);
        RefreshStatBars(statpoints.StatPoints);
    }

    void RefreshGear(GearSlots gearslots) {
        VisualElement container = _panel.Q<VisualElement>("GearSlots");
        foreach (var key in gearslots.Keys) {
            GearUISlot gearslot = container.Q<GearUISlot>(key.ToString());
            gearslot.Update(gearslots[key].Item);
        }
    }

    void RefreshStatBars(StatPoints stats) {
        foreach (var key in stats.AttackStats.Keys) {
            var statpoint = _panel.Q<VisualElement>($"{key} Attack");
            statpoint.Q<Label>("Key").text = $"{key} Attack";
            statpoint.Q<Label>("Value").text = stats.AttackStats[key].ToString();
        }

        foreach (var key in stats.DefenseStats.Keys) {
            var statpoint = _panel.Q<VisualElement>($"{key} Defense");
            statpoint.Q<Label>("Key").text = $"{key} Defense";
            statpoint.Q<Label>("Value").text = stats.DefenseStats[key].ToString();
        }
    }
}