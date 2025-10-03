using System.IO;

public static class FileWriter {
    public static void CreateFile(string filePath) {
        if (File.Exists(filePath)) 
            return;

        FileStream file = File.Create(filePath);
        file.Close();
    }

    public static void CreateDirectory(string directoryPath) {
        if (Directory.Exists(directoryPath)) 
            return;

        Directory.CreateDirectory(directoryPath);
    }
}
