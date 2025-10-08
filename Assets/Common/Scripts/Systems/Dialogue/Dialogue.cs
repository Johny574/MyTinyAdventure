
using System;
using UnityEngine;

[Serializable]
public struct Dialogue
{
    public Action FinishAction { get; private set; }
    public DialogueExchange[] Exchanges;

    public Dialogue(Action finishAction, DialogueExchange[] exchanges)
    {
        FinishAction = finishAction;
        Exchanges = exchanges;
    }
}

[Serializable]
public struct DialogueExchange
{
    [field:SerializeField] public string[] Lines { get; set; }
    [field:SerializeField] public Entity Speaker;

    public DialogueExchange(string[] lines, Entity speaker)
    {
        Lines = lines;
        Speaker = speaker;
    }
}