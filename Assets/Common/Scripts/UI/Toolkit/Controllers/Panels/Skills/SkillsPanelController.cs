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
    
    public void Open()
    {
        Enable();
        Refresh(Player.Instance.Gear.Gear.Gear, Player.Instance.Skills.Skills);
    }

    public override void Setup() {
        _ghostIcon = CreateGhostIcon(_ghostIcon_t);
        GearComponent gear = Player.Instance.Gear.Gear;
        SkillsComponent skills = Player.Instance.Skills.Skills;

        _fineprint = _panel.Q<Label>("Fineprint");
        //  create grid
        _gridView = new VisualElement();
        ScrollView scrollView = _panel.Q<ScrollView>("Slots-view");
        scrollView.Add(_gridView);

        // refresh skills
        gear.Updated += (gearslots) => Refresh(gearslots, skills);
        Refresh(gear.Gear, skills);
    }

    private void Refresh(GearSlots gear, SkillsComponent skills)
    {
        VisualElement skillslots = _root.Q<VisualElement>("Skills");
        _gridView.Clear();

        WeaponItemData primaryweapon = gear[GearItemSO.Slot.Primary].Item == null ? null : gear[GearItemSO.Slot.Primary].Item as WeaponItemData;
        WeaponItemData secondaryweapon = gear[GearItemSO.Slot.Secondary].Item == null ? null : gear[GearItemSO.Slot.Secondary].Item as WeaponItemData;

        RefreshWeaponSkills(skills, primaryweapon, skillslots);
        RefreshWeaponSkills(skills, secondaryweapon, skillslots);

        if (primaryweapon == null && secondaryweapon == null)
            _fineprint.style.visibility = Visibility.Visible;
        else
            _fineprint.style.visibility = Visibility.Hidden;
    }

    void RefreshWeaponSkills(SkillsComponent skills, WeaponItemData weapon, VisualElement slots)
    { 
        if (weapon == null)
            return;

        // loop over all skill classes
        foreach (var skillClass in weapon.SkillClasses)
        {
            VisualElement skillClassContainer = new VisualElement();
            skillClassContainer.style.width = Length.Auto();
            skillClassContainer.style.height = Length.Auto();

            // loop over all the skills inside class
            foreach (var skill in skillClass.Skills)
            {
                VisualElement slot = _slot_t.CloneTree();
                slot.dataSource = skill;
                slot.style.alignSelf = new StyleEnum<Align>(Align.FlexStart);

                DragAndDropManipulator dragAndDrop = new DragAndDropManipulator(_ghostIcon);
                slot.AddManipulator(dragAndDrop);
                var s = skill;

                dragAndDrop.DropSlot += () => slots.Children().Where(x => x.worldBound.Overlaps(this._ghostIcon.worldBound)).FirstOrDefault() as UISlot;

                dragAndDrop.DragStart += () =>
                {
                    _ghostIcon.Q<VisualElement>("Icon").style.backgroundImage = new StyleBackground(s.Icon);
                };

                dragAndDrop.OnDrop += (from, to) =>
                {
                    skills.Add(skill, Player.Instance.gameObject, slots.IndexOf(to));
                };

                dragAndDrop.DragStop += () =>
                {
                    _ghostIcon.style.visibility = Visibility.Hidden;
                };

                skillClassContainer.Add(slot);
            }
            _gridView.Add(skillClassContainer);
        }
    }
}