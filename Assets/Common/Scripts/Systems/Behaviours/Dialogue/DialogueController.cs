using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueController : Singleton<DialogueController>
{
    public bool Skip = false;
    [SerializeField] UIDocument _root;
    [SerializeField] VisualTreeAsset _dialogue_t;
    VisualElement _dialogueIcon, _skipButton;
    [SerializeField] VisualElement _dialogue_e;
    public string CurrentText = "";
    [SerializeField] AudioSource _dialogueAudio;
    Dialogue? _activeDialogue = null;
    SpriteRenderer _speaker;

    public Action OnClose;
    protected override void Awake() {
        base.Awake();
        VisualElement root = _root.rootVisualElement;
        var display = root.Q<VisualElement>("HUD-container");
        var templateContainer = _dialogue_t.CloneTree();
        _dialogue_e = templateContainer.Q<VisualElement>("Dialogue");
        _dialogue_e.style.position = new StyleEnum<Position>(Position.Absolute);
        _dialogue_e.style.visibility = Visibility.Hidden;
        _dialogueIcon = _dialogue_e.Q<VisualElement>("Icon");
        _skipButton = _dialogue_e.Q<VisualElement>("SkipButton");
        _skipButton.style.visibility = Visibility.Hidden;

        var start = _skipButton.transform.position;
        var stop = start + new Vector3(0, 10, 0);

        DOTween.To(() => start, x => {
            start = x;
            _skipButton.transform.position = x;
        }, stop, 1f
        ).SetLoops(-1, LoopType.Yoyo);

        display.Add(_dialogue_e);

        Label label = _dialogue_e.Q<Label>("Text");
        label.dataSource = this;
    }

    IEnumerator Play(Dialogue dialogue) {
        string displayedText;

        for (int i = 0; i < dialogue.Exchanges.Length; i++) {
            displayedText = "";
            var n = dialogue.Exchanges[i];
            _speaker = SceneTracker.Instance.Objects[typeof(Entity)].Find(x => x.GetComponent<Entity>().UID.Equals(n.Speaker.UID)).GetComponent<SpriteRenderer>();

            foreach (var line in dialogue.Exchanges[i].Lines) {
                for (int c = 0; c < line.Length; c++) {
                    displayedText += line[c];
                    yield return new WaitForSeconds(.01f);
                    CurrentText = displayedText;
                    _dialogueAudio.Play();
                }
            }
            _skipButton.style.visibility = Visibility.Visible;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || Skip);
            Skip = false;
            _skipButton.style.visibility = Visibility.Hidden;
        }    
        Close();
    }

    void Update() {
        if (_speaker != null)
            _dialogueIcon.style.backgroundImage = new StyleBackground(_speaker.sprite);
    }

    public void Open(Dialogue activeDialogue) {
        _activeDialogue = activeDialogue;
        _dialogue_e.style.visibility = Visibility.Visible;

        StartCoroutine(Play(activeDialogue));
        float start = -150f;
        float finish = 15f;
        DOTween.To(() => start, x => {
            start = x;
            _dialogue_e.style.bottom = new StyleLength(start);
        }, finish, .1f
        ).OnComplete(() => {
            _dialogue_e.style.bottom = finish;
        });
    }

    public void Close(bool finishAction = true) {
        StopAllCoroutines();
        float start = 15f;
        float finish = -150f;
        _speaker = null;

        DOTween.To(() => start, x => {
            start = x;
            _dialogue_e.style.bottom = new StyleLength(start);
        }, finish, .1f
        ).OnComplete(() => {
                _dialogue_e.style.visibility = Visibility.Hidden;
                if (finishAction)
                    _activeDialogue?.FinishAction?.Invoke();
                _activeDialogue = null;
                OnClose?.Invoke();
            }
        );
    }
}