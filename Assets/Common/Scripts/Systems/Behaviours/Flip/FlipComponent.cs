using UnityEngine;
public class FlipComponent : Component
{
    SpriteRenderer _renderer;
    public bool Flipped { get; set; } = false;
    GearComponent _gear;
    bool Invert = false;
    
    public FlipComponent(FlipBehaviour behaviour, SpriteRenderer renderer, bool invert) : base(behaviour) {
        _renderer = renderer;
        Invert = invert;
    }

    public void Initilize(GearComponent gear)
    {
        _gear = gear;
    }

    public void Flip(bool flip)
    {
        Flipped = flip;
        _renderer.flipX =  Invert ? flip : !flip; 
        foreach (var key in _gear.Gear.Keys)
            if (_gear.Gear[key].Object != null && _gear.Gear[key].Item != null && _gear.Gear[key].Renderer != null)
                _gear.Gear[key].Renderer.flipX = !flip;
    }
}