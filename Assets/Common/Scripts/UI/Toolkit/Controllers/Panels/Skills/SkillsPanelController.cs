using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class SkillsPanelController : PanelController
{
    VisualTreeAsset _slot_t, _ghostIcon_t;
    VisualElement _ghostIcon;
    VisualElement _gridView;
    Label _fineprint;

    public SkillsPanelController(VisualTreeAsset panel_t, VisualElement root, bool dragable,AudioSource closeaudio, AudioSource openaudio,  VisualTreeAsset slot_t, VisualTreeAsset ghostIcon_t) : base(panel_t, root, dragable, closeaudio, openaudio) {
        _slot_t = slot_t;
        _ghostIcon_t = ghostIcon_t;
    }

    public override void Setup() {
        _ghostIcon = CreateGhostIcon(_ghostIcon_t);
        GearComponent gear = Player.Instance.Gear.Gear;
        SkillsComponent skills = Player.Instance.Skills.Skills;

        //  create grid
        _gridView = new VisualElement();
        ScrollView scrollView = _panel.Q<ScrollView>("Slots-view");
        scrollView.Add(_gridView);

        // refresh skills
        gear.Updated += (gearslots) => Refresh(gearslots, skills);
        Refresh(gear.Gear, skills);
    }

    private void Refresh(GearSlots gear, SkillsComponent skills) {
        VisualElement skillslots = _root.Q<VisualElement>("Skills");
        _gridView.Clear();
        // loop over all gears
        foreach (var key in gear.Keys) {
            // todo : check if its assignable from weapon - this is o(n) where as there are only 2 slots so if u reference directly it will be o(2);
            if (gear[key].Item != null && typeof(WeaponItemData).IsAssignableFrom(gear[key].Item.GetType())) {
                WeaponItemData weapon = gear[key].Item as WeaponItemData;
                // loop over all skill classes
                foreach (var skillClass in weapon.SkillClasses) {
                    VisualElement skillClassContainer = new VisualElement();
                    skillClassContainer.style.width = Length.Auto();
                    skillClassContainer.style.height = Length.Auto();
                    
                    // loop over all the skills inside class
                    foreach (var skill in skillClass.Skills) {
                        VisualElement slot = _slot_t.CloneTree();
                        slot.dataSource = skill;
                        slot.style.alignSelf = new StyleEnum<Align>(Align.FlexStart);

                        DragAndDropManipulator dragAndDrop = new DragAndDropManipulator(_ghostIcon);
                        slot.AddManipulator(dragAndDrop);
                        var s = skill;

                        dragAndDrop.DropSlot += () => skillslots.Children().Where(x => x.worldBound.Overlaps(this._ghostIcon.worldBound)).FirstOrDefault() as UISlot;

                        dragAndDrop.DragStart += () => {
                            _ghostIcon.Q<VisualElement>("Icon").style.backgroundImage = new StyleBackground(s.Icon);
                        };

                        dragAndDrop.OnDrop += (from, to) => {
                            skills.Add(skill, Player.Instance.gameObject, skillslots.IndexOf(to));
                        };

                        dragAndDrop.DragStop += () => {
                            _ghostIcon.style.visibility = Visibility.Hidden;
                        };

                        skillClassContainer.Add(slot);
                    }
                    _gridView.Add(skillClassContainer);
                }
            }
        }
    }
}