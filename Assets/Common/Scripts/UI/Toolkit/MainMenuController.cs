using System.Linq;
using DG.Tweening;
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

    void SetupMainMenu()
    {
        _mainMenu = _mainMenu_t.CloneTree().Children().First();
        _uiDocument.rootVisualElement.Add(_mainMenu);
        var continueButton = _mainMenu.Q<Button>("Continue");
        var newgameButton = _mainMenu.Q<Button>("NewGame");
        var loadButton = _mainMenu.Q<Button>("Load");
        var optionsButton = _mainMenu.Q<Button>("Options");
        var exitButton = _mainMenu.Q<Button>("Exit");

        var start = Vector3.one;
        var end = new Vector3(1.1f, 1.1f, 1.1f);

        continueButton.RegisterCallback<PointerEnterEvent>((evt) => ButtonAnimation(continueButton, Vector3.one, end));
        RegisterLoadButton(continueButton, SaveSlot.AutoSave);
        newgameButton.RegisterCallback<PointerEnterEvent>((evt) => ButtonAnimation(newgameButton, Vector3.one, end));
        loadButton.RegisterCallback<PointerEnterEvent>((evt) => ButtonAnimation(loadButton, Vector3.one, end));
        optionsButton.RegisterCallback<PointerEnterEvent>((evt) => ButtonAnimation(optionsButton, Vector3.one, end));
        exitButton.RegisterCallback<PointerEnterEvent>((evt) => ButtonAnimation(exitButton, Vector3.one, end));

        continueButton.RegisterCallback<PointerLeaveEvent>((evt) => ButtonAnimation(continueButton, end, start));
        newgameButton.RegisterCallback<PointerLeaveEvent>((evt) => ButtonAnimation(newgameButton, end, start));
        loadButton.RegisterCallback<PointerLeaveEvent>((evt) => ButtonAnimation(loadButton, end, start));
        optionsButton.RegisterCallback<PointerLeaveEvent>((evt) => ButtonAnimation(optionsButton, end, start));
        exitButton.RegisterCallback<PointerLeaveEvent>((evt) => ButtonAnimation(exitButton, end, start));

        newgameButton.clicked += () => GameManager.Instance.StartNewGame();
        loadButton.clicked += () =>
        {
            _loadMenu.style.visibility = Visibility.Visible;
            _mainMenu.style.visibility = Visibility.Hidden;
        };
        exitButton.clicked += () => Application.Quit();        
    }


    void ButtonAnimation(VisualElement button, Vector3 s, Vector3 e)
    {
        Vector3 start = s;
        Vector3 end = e;
        DOTween.To(() => start, x =>
        {
            start = x;
            button.transform.scale = start;
        }, end, 1f);
    }

    
    void RegisterLoadButton(Button button, SaveSlot save)
    {
        if (Serializer.ContainsSave(save, "Player", ".json"))
        {
            button.clicked += () => GameManager.Instance.LoadSave(save);
            button.style.color = Color.white;
        }
        else
        {
            button.style.color = Color.grey;
        }
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
        var start = Vector3.one;
        var end = new Vector3(1.1f, 1.1f, 1.1f);

        autosaveButton.RegisterCallback<PointerEnterEvent>((evt) => ButtonAnimation(autosaveButton, Vector3.one, end));
        autosaveButton.RegisterCallback<PointerLeaveEvent>((evt) => ButtonAnimation(autosaveButton, end, start));
        RegisterLoadButton(autosaveButton, SaveSlot.AutoSave);

        save1Button.RegisterCallback<PointerEnterEvent>((evt) => ButtonAnimation(save1Button, Vector3.one, end));
        save1Button.RegisterCallback<PointerLeaveEvent>((evt) => ButtonAnimation(save1Button, end, start));
        RegisterLoadButton(save1Button, SaveSlot.Slot1);

        save2Button.RegisterCallback<PointerEnterEvent>((evt) => ButtonAnimation(save2Button, Vector3.one, end));
        save2Button.RegisterCallback<PointerLeaveEvent>((evt) => ButtonAnimation(save2Button, end, start));
        RegisterLoadButton(save2Button, SaveSlot.Slot2);

        save3Button.RegisterCallback<PointerEnterEvent>((evt) => ButtonAnimation(save3Button, Vector3.one, end));
        save3Button.RegisterCallback<PointerLeaveEvent>((evt) => ButtonAnimation(save3Button, end, start));
        RegisterLoadButton(save3Button, SaveSlot.Slot2);

        backButton.RegisterCallback<PointerEnterEvent>((evt) => ButtonAnimation(backButton, Vector3.one, end));
        backButton.RegisterCallback<PointerLeaveEvent>((evt) => ButtonAnimation(backButton, end, start));

        backButton.clicked += () => {
            _loadMenu.style.visibility = Visibility.Hidden;
            _mainMenu.style.visibility = Visibility.Visible;
        };
    }
}