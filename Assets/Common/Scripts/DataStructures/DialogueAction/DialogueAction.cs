
using System;
using UnityEngine;

[Serializable]
public struct ActiveDialogue { 
    [field:SerializeField] public string[] Dialogue { get; private set; }
    public Action FinishAction  { get; private set; } 
    public ActiveDialogue(string[] dialogue, Action finishAction) {
        Dialogue = dialogue;
        FinishAction = finishAction;
    }
}