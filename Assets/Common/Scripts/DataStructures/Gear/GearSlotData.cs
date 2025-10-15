


using System;

[Serializable]
public struct GearSlotData
{
    public GearItemSO.Slot _Slot;
    public string ItemGUID;

    public GearSlotData(GearItemSO.Slot slot, string itemGUID) {
        _Slot = slot;
        ItemGUID = itemGUID;
    }
}