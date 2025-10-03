using System;
using System.Collections.Generic;
using UnityEngine;

public class EmoteComponent : Component {
    public IUndoCommand Emote;
    private Queue<IUndoCommand> _emotes = new();
    public float Timer = 0f, Duration = 2f;
    bool _remove = true;
    Emote _emote;
    public EmoteComponent(EmoteBehaviour behaviour, Emote emote, bool remove) : base(behaviour) {
        _remove = remove;
        _emote = emote;
    }

    public void Add(Sprite emote) {
        if (Emote == null) {
            Emote = new EmoteCommands.DisplayCommand(_emote, emote, Behaviour.gameObject);
            Emote.Execute();
        }
        else 
            _emotes.Enqueue(new EmoteCommands.DisplayCommand(_emote, emote, Behaviour.gameObject));
    }

    public void Remove() {
        if (Emote == null) 
            return;

        Emote.Undo();
        Emote = _emotes.Count > 0 ? _emotes.Dequeue() : null;
        Emote?.Execute();
    }
    
    public void Update() {
        if (Emote == null || !_remove) 
            return;
        
        if (Timer < Duration) 
            Timer += Time.deltaTime;
        else {
            Remove();
            Timer = 0f;
        }
    }
}