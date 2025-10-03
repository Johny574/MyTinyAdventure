using UnityEngine;

namespace EmoteCommands {
    public class DisplayCommand : IUndoCommand {
        Sprite _emoteSprite;
        GameObject _following;

        Emote _emote;

        public DisplayCommand(Emote emote, Sprite emotesprite, GameObject following) {
            _emoteSprite = emotesprite;
            _following = following;
            _emote = emote;
        }

        public void Execute() {
            Display();
        }

        public void Undo() {
            _emote.gameObject.SetActive(false);
        }

        void Display()
        {
            _emote.Bind(_emoteSprite);
            _emote.gameObject.SetActive(true);
        }
    }
}