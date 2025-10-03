using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class DeathMenu : MonoBehaviour
{
    [SerializeField] VisualTreeAsset _deathMenu_t;
    [SerializeField] UIDocument _document;
    VisualElement _deathMenu_e;

    void Awake() {
        _deathMenu_e = _deathMenu_t.CloneTree().Children().First();
        _document.rootVisualElement.Add(_deathMenu_e);
        _deathMenu_e.style.visibility = Visibility.Hidden;
        Player.Instance.Health.Health.Death += OnDeath;

        var loadButton = _deathMenu_e.Q<Button>("Load");
        var menuButton = _deathMenu_e.Q<Button>("Menu");

        loadButton.clicked += LoadClicked;
        menuButton.clicked += MainMenuClicked;
    }

    void OnDeath() => _deathMenu_e.schedule.Execute(() => _deathMenu_e.style.visibility = Visibility.Visible).StartingIn(2000);
    void LoadClicked() => GameManager.Instance.LoadSave(SaveSlot.AutoSave);
    void MainMenuClicked() => GameManager.Instance.MainMenu();
}