using UnityEngine;

[DefaultExecutionOrder(100)]
public class PlayerSave : SerializedObject<PlayerSaveData>
{
    [SerializeField] Sprite _minimapMarker;
    
    protected override void Start() {
        base.Start();
        MiniMapController.Instance.Register(gameObject, _minimapMarker);   
    }

    public override void Load(PlayerSaveData save) {
        Player.Instance.Health.Health.Load(save.Health);
        Player.Instance.Mana.Mana.Load(save.Mana);
        Player.Instance.Inventory.Inventory.Load(save.Inventory);
        Player.Instance.Experience.Experience.Load(save.Experience);
        Player.Instance.Currency.Currency.Load(save.Currency);
        Player.Instance.Buffs.Buffs.Load(save.Buffs);
        Player.Instance.Skills.Skills.Load(save.Skills);
        Player.Instance.Gear.Gear.Load(save.Gear);
        Player.Instance.Journal.Questing.Load(save.Journal);
        Player.Instance.Consumables.Consumables.Load(save.Consumables);
        Player.Instance.Location.Location.Load(new Vector2(save.X, save.Y));
    }

    public override PlayerSaveData Save() {
        Vector2 location = Player.Instance.Location.Location.Save();

        return new PlayerSaveData
        (
            Player.Instance.Health.Health.Save(),
            Player.Instance.Mana.Mana.Save(),
            Player.Instance.Stamina.Stamina.Save(),
            Player.Instance.Experience.Experience.Save(),
            Player.Instance.Stats.Stats.Save(),
            Player.Instance.Inventory.Inventory.Save(),
            Player.Instance.Currency.Currency.Save(),
            Player.Instance.Buffs.Buffs.Save(),
            Player.Instance.Skills.Skills.Save(),
            Player.Instance.Gear.Gear.Save(),
            Player.Instance.Journal.Questing.Save(),
            Player.Instance.Consumables.Consumables.Save(),

            location.x,
            location.y,
            Player.Instance.Location.Location.CurrentScene
        );
    }
}