using UnityEngine;

public class Emote : MonoBehaviour
{
    public SpriteRenderer Sprite;
    public void Bind(Sprite variant) => Sprite.sprite = variant;
}
