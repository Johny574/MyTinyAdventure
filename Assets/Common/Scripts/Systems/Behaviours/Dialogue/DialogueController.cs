using System.Collections;
using DG.Tweening;
using FletcherLibraries;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueController : Singleton<DialogueController>
{
    public bool Skip = false;
    [SerializeField] UIDocument _root;
    [SerializeField] VisualTreeAsset _dialogue_t;
    VisualElement _dialogueIcon;
    [SerializeField] VisualElement _dialogue_e;
    public string CurrentText = "";
    [SerializeField] AudioSource _dialogueAudio;
    Dialogue? _activeDialogue = null;
    GameObject _dialogue;

    protected override void Awake() {
        base.Awake();
        VisualElement root = _root.rootVisualElement;
        var display = root.Q<VisualElement>("HUD-container");
        var templateContainer = _dialogue_t.CloneTree();
        _dialogue_e = templateContainer.Q<VisualElement>("Dialogue");
        _dialogue_e.style.position = new StyleEnum<Position>(Position.Absolute);
        _dialogue_e.style.visibility = Visibility.Hidden;
        _dialogueIcon = _dialogue_e.Q<VisualElement>("Icon");  

        display.Add(_dialogue_e);

        Label label = _dialogue_e.Q<Label>("Text");
        label.dataSource = this;
    }

    IEnumerator Play(Dialogue dialogue) {
        string displayedText;
        for (int i = 0; i < dialogue.Dialogue.Length; i++) {
            displayedText = "";
            for (int c = 0; c < dialogue.Dialogue[i].Length ; c++) {
                displayedText += dialogue.Dialogue[i][c];
                yield return new WaitForSeconds(.1f);
                CurrentText = displayedText;
                _dialogueAudio.Play();
            }

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || Skip);
            Skip = false;
        }
        //   dialogue.FinishAction?.Invoke();
        // Clear();
        // GameMenuStatemachine.Instance.ChangeState("None");
        Close();
    }

    void Update() {
        if (_dialogue != null)
            _dialogueIcon.style.backgroundImage = new StyleBackground(_dialogue.GetComponent<SpriteRenderer>().sprite);
    }

    public void Open(Dialogue activeDialogue, GameObject dialogue) {
        _activeDialogue = activeDialogue;
        _dialogue_e.style.visibility = Visibility.Visible;
        _dialogue = dialogue;

        StartCoroutine(Play(activeDialogue));
        float start = -150f;
        float finish = 15f;
        DOTween.To(() => start, x => {
            start = x;
            _dialogue_e.style.bottom = new StyleLength(start);
        }, finish, .1f
        ).OnComplete(() => _dialogue_e.style.bottom = finish);
    }

    public void Close(bool finishAction = true) {
        StopAllCoroutines();
        float start = 15f;
        float finish = -150f;
        _dialogue = null;

        DOTween.To(() => start, x => {
            start = x;
            _dialogue_e.style.bottom = new StyleLength(start);
        }, finish, .1f
        ).OnComplete(() => {
                _dialogue_e.style.visibility = Visibility.Hidden;
                if (finishAction)
                    _activeDialogue?.FinishAction?.Invoke();
                _activeDialogue = null;
            }
        );
    }
}