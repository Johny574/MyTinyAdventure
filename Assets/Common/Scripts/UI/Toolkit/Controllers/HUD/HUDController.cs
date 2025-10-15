using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class HUDController : MonoBehaviour
{
    UIDocument _hud;
    VisualElement _root;
    [SerializeField] VisualTreeAsset _quest_t, _queststep_t;
    [SerializeField] RenderTexture _minimap;
    VisualElement _interactdisplay;
    VisualElement _playerIcon;

    void Awake() => _hud = GetComponent<UIDocument>();
    void Start() {
        _root = _hud.rootVisualElement;
        _interactdisplay = _root.Q<VisualElement>("Interact");
        _playerIcon = _root.Q<VisualElement>("Player-display").Q<VisualElement>("Icon").Children().First();
        SetupPlayerDisplay(
            _root,
            Player.Instance.Health.Health,
            Player.Instance.Stamina.Stamina,
            Player.Instance.Mana.Mana,
            Player.Instance.Experience.Experience,
            Player.Instance.Currency.Currency
        );

        SetupCache(_root, Player.Instance.Cache.Cache);
        SetupMinimap(_root);
        SetupQuests(_root, Player.Instance.Journal.Questing);
        SetupHotbar(_root, Player.Instance.Skills.Skills, Player.Instance.Consumables.Consumables, Player.Instance.Gear.Gear);
        SetupInteract(_root, Player.Instance.Interact.Interact);
    }

    void Update() {
        VisualElement hotbar = _root.Q<VisualElement>("Hotbar");
        VisualElement skillslots = hotbar.Q<VisualElement>("Skills");
        VisualElement attackslots = hotbar.Q<VisualElement>("Consumables");
        VisualElement consumableslots = hotbar.Q<VisualElement>("Consumables");

        foreach (var child in skillslots.Children()) {
            HotbarUISlot slot = child as HotbarUISlot;
            slot.Tick();
        }

        foreach (var child in consumableslots.Children()) {
            ConsumableUISlot slot = child as ConsumableUISlot;
            slot.Tick();
        }

        _playerIcon.style.backgroundImage = new StyleBackground(Player.Instance.Renderer.sprite);
    }

    void SetupPlayerDisplay(VisualElement root, HealthComponent health, StaminaComponent stamina, ManaComponent mana, ExperienceComponent xp, CurrencyComponent currency) { 
        var healthbar = root.Q<VisualElement>("Health-bar");
        var staminabar = root.Q<VisualElement>("Stamina-bar");
        var manabar = root.Q<VisualElement>("Mana-bar");
        var xpbar = root.Q<VisualElement>("XP-bar");
        var currencydisplay = root.Q<VisualElement>("Currency");

        healthbar.dataSource = health;
        staminabar.dataSource = stamina;
        manabar.dataSource = mana;
        xpbar.dataSource = xp;

        currencydisplay.dataSource = currency.Currency;
    }

    void SetupCache(VisualElement root, CacheComponent cache) {
        var cachedisplay = root.Q<VisualElement>("Cache");  
        cache.Changed += (entity) => {
            float start = 0;
            float finish = -100;

            if (entity != null) {
                start = -100;
                finish = 0;
            }
            
            DOTween.To(() => start, x => {
                    start = x;
                    cachedisplay.style.top = new StyleLength(start);
                }, finish, 1f
            );
        };
    }

    void SetupMinimap(VisualElement root) {
        var minimap = root.Q<VisualElement>("Minimap").Q<VisualElement>("Map");
        var parent = minimap.Q<VisualElement>("Image");
        Image image = new Image();
        image.style.width = new StyleLength(Length.Percent(100));
        image.style.height= new StyleLength(Length.Percent(100));
        image.image = _minimap;
        parent.hierarchy.Add(image);
        Button minimapzoomin = root.Q<Button>("MinimapZoomIn");
        Button minimapzoomout = root.Q<Button>("MinimapZoomOut");
    }

    void SetupQuests(VisualElement root, QuestingComponent journal) {
        VisualElement container = root.Q<VisualElement>("Quests-container");
        VisualElement scrollview = container.Q<ScrollView>("ScrollView");
        VisualElement gridview = new VisualElement();

        Button questsminimize = root.Q<Button>("QuestMaximize");
        Button questsmaximize = root.Q<Button>("QuestMinimize");

        questsminimize.clicked += () => {
            float start = 100f;
            float finish = 0f;
            DOTween.To(() => start, x => {
                    start = x;
                    container.style.height = new StyleLength(Length.Percent(start));
                }, finish, 1f
            ).OnComplete(() => container.style.visibility = Visibility.Hidden);
        };

        questsmaximize.clicked += () => {
            container.style.visibility = Visibility.Visible;
            float start = 0f;
            float finish = 100f;
            DOTween.To(() => start, x => {
                    start = x;
                    container.style.height = new StyleLength(Length.Percent(start));
                }, finish, 1f
            );
        };

        gridview.style.width = new StyleLength(Length.Percent(100));
        gridview.style.height = new StyleLength(Length.Percent(100));

        scrollview.Add(gridview);
        
        scrollview.schedule.Execute(() => QuestsRefresh(journal, gridview, _quest_t, _queststep_t)).StartingIn(1000);
        journal.StepCompleted += (step) =>QuestsRefresh(journal, gridview, _quest_t, _queststep_t);
    }

    void SetupHotbar(VisualElement root, SkillsComponent skills, ConsumableComponent consumables, GearComponent gear) {
        VisualElement hotbar = root.Q<VisualElement>("Hotbar");
        VisualElement skillslots = hotbar.Q<VisualElement>("Skills");
        VisualElement attackslots = hotbar.Q<VisualElement>("Consumables");
        VisualElement consumableslots = hotbar.Q<VisualElement>("Consumables");

        skills.Updated += (skills) => RefreshHotbarSkills(skills, skillslots);

        foreach (var child in skillslots.Children()) {
            var slot = child as HotbarUISlot;
            child.RegisterCallback<MouseDownEvent>(evt => {
                Player.Instance.Skills.Skills.Remove(slot.Index);
                RefreshHotbarSkills(skills.Skills, skillslots);
            });
        }

        foreach (var child in consumableslots.Children()) {
            var slot = child as ConsumableUISlot;
            child.RegisterCallback<MouseDownEvent>(evt => {
                Player.Instance.Consumables.Consumables.Remove(slot.Index);
                RefreshHotbarConsumables(consumables.Consumables, consumableslots);
            });
        }

        RefreshHotbarSkills(skills.Skills, skillslots);

        gear.Updated += (gear) => RefreshHotbarAttack(gear, attackslots);

        consumables.Updated += (consumables) => RefreshHotbarConsumables(consumables, consumableslots);
        RefreshHotbarConsumables(consumables.Consumables, consumableslots);
    }

    void RefreshHotbarAttack(GearSlots gear, VisualElement container) {
        int index = 0;
        
        foreach (var child in container.Children())
        {
            var slot = child as AttackUISlot;
            slot.Clear();
            if (index == 0 && gear[GearItemSO.Slot.Primary] != null)
                slot.Update(gear[GearItemSO.Slot.Primary].Item as WeaponItemSO);
            else if (index == 1 && gear[GearItemSO.Slot.Secondary] != null)
                slot.Update(gear[GearItemSO.Slot.Secondary].Item as WeaponItemSO);
        }
    }

    void RefreshHotbarConsumables(Consumable[] consumables, VisualElement container) {
        foreach (var child in container.Children()) {
            ConsumableUISlot slot = child as ConsumableUISlot;
            slot.Update(consumables[slot.Index]);
        }
    }

    void RefreshHotbarSkills(Skill[] skills, VisualElement container) {
        foreach (var child in container.Children()) {
            HotbarUISlot slot = child as HotbarUISlot;
            slot.Clear();
            slot.Update(skills[slot.Index]);
        }
    }

    void SetupInteract(VisualElement root, InteractComponent interact) {
        _interactdisplay.style.visibility = Visibility.Visible;
        interact.TargetChanged += (target) => InteractRefresh(target, _interactdisplay);
        _interactdisplay.style.left = new (-100);
        _interactdisplay.dataSource = Player.Instance.Interact.Interact;
    }

    void InteractRefresh(IInteractable target, VisualElement interact) {
        var start = target == null ? 0 : -100;
        var end = target == null ? -100 : 0;
        DOTween.To(
            () => start, x => {
                start = x;
                interact.style.left = new(x);
            }, end, .5f
        );
    }

    void QuestsRefresh(QuestingComponent journal, VisualElement container, VisualTreeAsset questentry_t, VisualTreeAsset step_t) {
        container.Clear();

        foreach (var quest in journal.ActiveQuests) {
            VisualElement questentry = questentry_t.CloneTree();
            questentry.Q<Label>("Head").text = quest.SO.Description;
            questentry.dataSource = quest;
            int stepcount = 0;

            foreach (var step in quest.Steps) {
                VisualElement stepslot = step_t.CloneTree();
                questentry.Add(stepslot);
                stepslot.style.flexGrow = new StyleFloat(0f);
                stepslot.style.flexShrink= new StyleFloat(1f);
                stepslot.dataSource = step;
                var label = stepslot.Q<Label>("Step");
                label.text = step.SO.Description;

                if (quest.Index < stepcount)
                    label.style.color = Color.black;
                else if (quest.Index == stepcount)
                    label.style.color = Color.red;
                else
                    label.style.color = Color.white;
                    
                stepcount++;
            }

            container.Add(questentry);
        }
    }
}