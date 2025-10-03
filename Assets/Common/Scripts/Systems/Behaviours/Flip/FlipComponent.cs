using UnityEngine;
public class FlipComponent : Component
{
    SpriteRenderer _renderer;
    public bool Flipped { get; set; } = false;
    public FlipComponent(FlipBehaviour behaviour, SpriteRenderer renderer) : base(behaviour) {
        _renderer = renderer;
    }
    public void Flip(bool flip) {
        Flipped = flip;
        _renderer.flipX = !flip;
    }
}