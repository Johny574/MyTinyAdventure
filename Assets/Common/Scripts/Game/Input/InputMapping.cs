using System;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class InputMapping {
    public InputActionReference Action;
    public Sprite Icon;

    public InputMapping(InputActionReference action, Sprite icon) {
        Action = action;
        Icon = icon;
    }
}