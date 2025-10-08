using UnityEngine;

public class Storage : Container{
    void OnDisable() {
        Serializer.SaveFile(Inventory.Save(), "Storage.json", SaveSlot.AutoSave);
    }
    
     protected void Start() {
        if (Serializer.ContainsSave(SaveSlot.AutoSave, "Storage", ".json")) {
            var inventory = Serializer.LoadFile<ItemStackData[]>("Storage.json", SaveSlot.AutoSave);
            Inventory.Load(inventory);
        }
    }
}