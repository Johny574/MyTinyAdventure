using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public static class Serializer   {
    public static string AppRelativeSavePath { get; private set; } = Path.Join(Application.dataPath, "Save");
    public static void SaveFile<T>(T obj, string filename, SaveSlot slot) {
        var saveSlotDirectory = Path.Join(AppRelativeSavePath, slot.ToString());

        FileWriter.CreateDirectory(saveSlotDirectory);

        FileWriter.CreateFile(Path.Join(saveSlotDirectory, filename));

        using (FileStream filestream = File.Open(Path.Join(saveSlotDirectory, filename), FileMode.Truncate)) {
            using (StreamWriter writer = new StreamWriter(filestream)) {
                string serializedObj = JsonConvert.SerializeObject(obj, Formatting.Indented);
                writer.Write(serializedObj);
                writer.Flush();
            }
        }
    }
      
    public static T LoadFile<T>(string file, SaveSlot slot) {
        var saveSlotDirectory = Path.Join(AppRelativeSavePath, slot.ToString());
        FileWriter.CreateDirectory(saveSlotDirectory);
  
        var saveFilePath = Path.Join(saveSlotDirectory, file);

        FileWriter.CreateFile(saveFilePath);            

        FileStream fileStream = File.Open(saveFilePath, FileMode.Open);
        using (StreamReader reader = new StreamReader(fileStream)) {
            string json = reader.ReadToEnd(); 
            return JsonConvert.DeserializeObject<T>(json);
        }
    }

    public static void DeleteSave(SaveSlot slot) {
        if (!Directory.Exists(Path.Join(AppRelativeSavePath, slot.ToString()))) 
            return;

        Directory.Delete(Path.Join(AppRelativeSavePath, slot.ToString()), true);
    }

    public static bool ContainsSave(SaveSlot slot, string saveName, string fileExtension) => File.Exists(AppRelativeSavePath + "/" + slot.ToString() + "/" +  saveName + fileExtension);
}

public class SerializeException : Exception {
    public SerializeException(string message) : base(message) {

    }
}

public enum SaveSlot {
    AutoSave,
    Slot1,
    Slot2,
}