using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class SkillsPanelController : PanelController
{
    VisualTreeAsset _slot_t, _ghostIcon_t;
    VisualElement _ghostIcon;
    VisualElement _gridView;
    Label _fineprint;
    VisualElement _hotbarslots, _panelSlots;

    public SkillsPanelController(VisualTreeAsset panel_t, VisualElement root, bool dragable, AudioSource closeaudio, AudioSource openaudio, VisualTreeAsset slot_t, VisualTreeAsset ghostIcon_t) : base(panel_t, root, dragable, closeaudio, openaudio)
    {
        _slot_t = slot_t;
        _ghostIcon_t = ghostIcon_t;
    }
    
    public override void Open()
    {
        Enable();
        Refresh(Player.Instance.Gear.Gear.Gear);
    }

    public override void Setup() {
        _ghostIcon = CreateGhostIcon(_ghostIcon_t);
        GearComponent gear = Player.Instance.Gear.Gear;
        SkillsComponent skills = Player.Instance.Skills.Skills;

        _fineprint = _panel.Q<Label>("Fineprint");
        _gridView = new VisualElement();
        _gridView.style.height = new StyleLength(Length.Percent(100));
        _gridView.style.width = new StyleLength(Length.Percent(100));
        ScrollView scrollView = _panel.Q<ScrollView>("ScrollView");
        scrollView.Add(_gridView);

        gear.Updated += (gearslots) => Refresh(gearslots);
        Refresh(gear.Gear);
         _fineprint.style.position = Position.Relative;
        _fineprint.style.display = DisplayStyle.None;
        _fineprint.style.visibility = Visibility.Hidden;
        _hotbarslots = _root.Q<VisualElement>("Skills");
        _panelSlots = _panel.Q<VisualElement>("Slots");
        scrollView.contentContainer.style.flexGrow = 1;
    }

    void Refresh(GearSlots gear)
    {
        _gridView.Clear();

        WeaponItemSO primaryweapon = gear[GearItemSO.Slot.Primary].Item == null ? null : gear[GearItemSO.Slot.Primary].Item as WeaponItemSO;
        WeaponItemSO secondaryweapon = gear[GearItemSO.Slot.Secondary].Item == null ? null : gear[GearItemSO.Slot.Secondary].Item as WeaponItemSO;

        RefreshWeaponSkills(primaryweapon, _hotbarslots);
        RefreshWeaponSkills(secondaryweapon, _hotbarslots);

        if (primaryweapon == null && secondaryweapon == null && Enabled)
        {
            _fineprint.style.display = DisplayStyle.Flex;
            _fineprint.style.visibility = Visibility.Visible;
            _fineprint.style.position = Position.Absolute;
        }
        else
        {
            _fineprint.style.position = Position.Relative;
            _fineprint.style.display = DisplayStyle.None;
            _fineprint.style.visibility = Visibility.Hidden;
        }
    }

    protected override void OnPanelClosed()
    {
        Debug.Log("a");
        if (_fineprint == null)
            Debug.LogWarning("_fineprint is null!");
        else
            _fineprint.style.visibility = Visibility.Hidden;
    }

    void RefreshWeaponSkills(WeaponItemSO weapon, VisualElement slots)
    { 
        if (weapon == null)
            return;

        // loop over all skill classes
        foreach (var skillClass in weapon.SkillClasses)
        {
            VisualElement skillClassContainer = new VisualElement();
            skillClassContainer.style.width = Length.Auto();
            skillClassContainer.style.height = new StyleLength(Length.Percent(100));
            skillClassContainer.style.justifyContent = Justify.FlexEnd;

            // loop over all the skills inside class
            foreach (var skill in skillClass.Skills)
            {
                VisualElement slot = _slot_t.CloneTree();
                slot.dataSource = skill;
                slot.style.alignSelf = new StyleEnum<Align>(Align.FlexStart);

                DragAndDropManipulator dragAndDrop = new DragAndDropManipulator(_ghostIcon);
                slot.AddManipulator(dragAndDrop);
                var s = skill;

                dragAndDrop.DropSlot += DropSlot;

                dragAndDrop.DragStart += () =>
                {
                    _ghostIcon.Q<VisualElement>("Icon").style.backgroundImage = new StyleBackground(s.Icon);
                };

                dragAndDrop.OnDrop += (from, to) =>
                {
                    to.OnDrop(this);
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
     UISlot DropSlot() {
        var hotbarslot = _hotbarslots.Children().SelectMany(x => x.Children()).Where(x => x.worldBound.Contains(this._ghostIcon.worldBound.center)).FirstOrDefault() as UISlot;
        if (hotbarslot != null)
            return hotbarslot;

        var panelSlot = _panelSlots.Children().Where(x => x.worldBound.Overlaps(this._ghostIcon.worldBound)).FirstOrDefault() as UISlot;
        if (panelSlot != null)
            return panelSlot;

        return null;
    } 
}