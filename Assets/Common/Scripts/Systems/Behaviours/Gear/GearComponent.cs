using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class GearComponent : Component, ISerializedComponent<GearSlotData[]>
{
    public GearSlots Gear;
    public Action<GearItemSO> Equiped, Unequiped;
    public Action<GearSlots> Updated;

    public GearComponent(GearBehaviour behaviour, GearSlots gear) : base(behaviour) {
        Gear = gear;
    }

    public void Initilize(HealthComponent health) {
        health.Death += () => {
            foreach (var key in Gear.Keys) {
                Gear[key].Object.SetActive(false);
            }
        };

        foreach (var key in Gear.Keys) {
            if (Gear[key].Object != null && Gear[key].Item != null) {
                Gear[key].Object.GetComponent<SpriteRenderer>().sprite = Gear[key].Item.Sprite;
                Gear[key].Object.gameObject.SetActive(true);
            }
            else 
                Gear[key].Object.gameObject.SetActive(false);
        }
    }

    public void Equipt(ItemSO item) {
        if (!typeof(GearItemSO).IsInstanceOfType(item)) 
            return;


        GearItemSO gearitem = item as GearItemSO;

        UnEquipt(gearitem.Target);

        if (typeof(WeaponItemData).IsAssignableFrom(gearitem.GetType())) {
            // Weapons[item.ID].Object.GetComponent<Animator>().runtimeAnimatorController = ((WeaponItemData)item).Animation;
        }

        Gear[gearitem.Target].Item = gearitem;

        if (Gear[gearitem.Target].Object != null && Gear[gearitem.Target].Item != null) {
            Gear[gearitem.Target].Object.GetComponent<SpriteRenderer>().sprite = item.Sprite;
            Gear[gearitem.Target].Object.gameObject.SetActive(true);
        }

        Equiped?.Invoke(gearitem);
        Updated?.Invoke(Gear);
    }

    public void UnEquipt(GearItemSO.Slot slot) {
        if (Gear[slot].Item != null) {
            Unequiped?.Invoke(Gear[slot].Item);
        }

        Gear[slot].Item = null;

        if (Gear[slot].Object != null && Gear[slot].Item == null) {
            Gear[slot].Object.gameObject.SetActive(false);

        }
        Updated?.Invoke(Gear);
    }

    public GearSlotData[] Save() {
        return Gear.Where(x => x.Value?.Item != null).Select(x => new GearSlotData(x.Key, x.Value.Item.GUID)).ToArray();
    }

    public void Load(GearSlotData[] save) {
        for (int i = 0; i < save.Length; i++) {
            GearSlotData gearslot = save[i]; // keep this reference in memory

            UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GearItemSO> ItemSO = Addressables.LoadAssetAsync<GearItemSO>(new AssetReference(gearslot.ItemGUID));
            ItemSO.Completed += (itemso) => {
                if (itemso.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Failed) {
                    throw new System.Exception($"Failed to load asset {gearslot.ItemGUID}");
                }
                if (itemso.Result != null) {
                    Equipt(itemso.Result);
                }
            };
        }
    }
}