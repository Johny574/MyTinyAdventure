


using System;

[Serializable]
public struct GearSlotData
{
    public GearItemSO.Slot Slot;
    public string ItemGUID;

    public GearSlotData(GearItemSO.Slot slot, string itemGUID) {
        Slot = slot;
        ItemGUID = itemGUID;
    }
}