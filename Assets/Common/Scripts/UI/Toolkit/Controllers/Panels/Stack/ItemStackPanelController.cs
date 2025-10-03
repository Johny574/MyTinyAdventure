using System;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemStackPanelController : PanelController
{
    ItemStackCommand _stack;
    Action _confirmAction;
    public ItemStackPanelController(VisualTreeAsset panel_t, VisualElement root, bool dragable, AudioSource openaudio, AudioSource closeaudio) : base(panel_t, root, dragable, openaudio, closeaudio) {

    }

    public override void Setup() {
        Button confirm = _panel.Q<Button>("Confirm");
        Button cancel = _panel.Q<Button>("Close");
        Button increment = _panel.Q<Button>("Increment");
        Button decrement = _panel.Q<Button>("Decrement");
        TextField input = _panel.Q<TextField>("Input");

        // _panel.dataSource = _stack?.Stack;
        
        input.value = _stack?.Stack.Count.ToString();

        confirm.clicked += () => {
            _confirmAction.Invoke();
            Disable();
        };

        increment.clicked += () => {
            _stack?.Stack.Update(1);
            input.value = _stack?.Stack.Count.ToString();
        };

        decrement.clicked += () => {
            _stack?.Stack.Update(-1);
            input.value = _stack?.Stack.Count.ToString();
        };
    }

    public void Open(ItemStackCommand stack, Action confirmAction) {
        // todo : move to global variables
        TextField input = _panel.Q<TextField>("Input");
        Label name = _panel.Q<Label>("Name");
        VisualElement icon = _panel.Q<VisualElement>("Icon");
        icon.style.backgroundImage = new StyleBackground(stack.Stack.Item.Sprite);
        name.text = stack.Stack.Item.DisplayName;
        _stack = stack;
        _panel.BringToFront();
        input.value = _stack?.Stack.Count.ToString();
        Enable();
        _confirmAction = confirmAction;
    }

}