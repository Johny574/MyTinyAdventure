
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class HotbarUISlot : UISlot
{
    [UxmlAttribute]
    public int Index;
    Skill _skill;
    VisualElement _fill;

    public HotbarUISlot() {
        AddToClassList("hotbar-slot");
        AddToClassList("slot-medium");

        _fill = new VisualElement();
        _fill.style.backgroundColor = new Color(0f, 0f, 0f, .5f);
        _fill.style.width = new StyleLength(Length.Percent(100));
        _fill.style.height = new StyleLength(Length.Percent(0));
        _fill.name = "fill";
        _fill.style.bottom = new StyleLength(0f);
        _fill.style.alignSelf = new StyleEnum<Align>(Align.Center);
        _fill.style.position = Position.Absolute;
        Add(_fill);
    }

    public override void OnDrop<T>(T data) {
        if (typeof(T) != typeof(SkillSO) || data == null)
            return;

        SkillSO skill = (SkillSO)(object)data;
        
        Player.Instance.Skills.Skills.Add(skill, Player.Instance.gameObject, Index);
    }

    public void Tick() {
        if (_skill == null)
            return;
        _fill.style.height = new StyleLength(Length.Percent(_skill.Fill * 100));
    }

    public override void Update<T>(T data) {
        if (data == null)
            return;

        _skill = (Skill)(object)data;
        Icon.style.backgroundImage = new StyleBackground(_skill.Data.Icon);
    }
}