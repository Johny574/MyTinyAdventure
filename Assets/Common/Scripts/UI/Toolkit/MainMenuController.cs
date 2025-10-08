using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] VisualTreeAsset _mainMenu_t, _loadMenu_t;
    VisualElement _mainMenu, _loadMenu;
    [SerializeField] UIDocument _uiDocument;

    void Awake() {
        SetupMainMenu();
        SetupLoadMenu();
    }

    void SetupMainMenu() {
        _mainMenu = _mainMenu_t.CloneTree().Children().First();
        _uiDocument.rootVisualElement.Add(_mainMenu);
        var continueButton = _mainMenu.Q<Button>("Continue");
        var newgameButton = _mainMenu.Q<Button>("NewGame");
        var loadButton = _mainMenu.Q<Button>("Load");
        var optionsButton = _mainMenu.Q<Button>("Options");
        var exitButton = _mainMenu.Q<Button>("Exit");

        continueButton.clicked += () => GameManager.Instance.LoadSave(SaveSlot.AutoSave);
        newgameButton.clicked += () => GameManager.Instance.StartNewGame();
        newgameButton.clicked += () => Debug.Log("Deesnuts");
        loadButton.clicked += () => {
            _loadMenu.style.visibility = Visibility.Visible;
            _mainMenu.style.visibility = Visibility.Hidden;
        };
        exitButton.clicked += () => Application.Quit();
    }

    void SetupLoadMenu() {
        _loadMenu = _loadMenu_t.CloneTree().Children().First();
        _uiDocument.rootVisualElement.Add(_loadMenu);
        _loadMenu.style.visibility = Visibility.Hidden;

        var autosaveButton = _loadMenu.Q<Button>("Autosave");
        var save1Button = _loadMenu.Q<Button>("Slot1");
        var save2Button = _loadMenu.Q<Button>("Slot2");
        var save3Button = _loadMenu.Q<Button>("Slot3");
        var backButton = _loadMenu.Q<Button>("Back");

        autosaveButton.clicked += () => GameManager.Instance.LoadSave(SaveSlot.AutoSave);
        save1Button.clicked += () => GameManager.Instance.LoadSave(SaveSlot.Slot1);
        save2Button.clicked += () => GameManager.Instance.LoadSave(SaveSlot.Slot2);
        save3Button.clicked += () => GameManager.Instance.LoadSave(SaveSlot.Slot2);
        backButton.clicked += () => {
            _loadMenu.style.visibility = Visibility.Hidden;
            _mainMenu.style.visibility = Visibility.Visible;
        };
    }
}