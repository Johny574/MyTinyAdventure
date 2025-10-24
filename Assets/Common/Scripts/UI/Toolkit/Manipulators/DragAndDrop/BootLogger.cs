using UnityEngine;
using System.IO;

public class BootLogger : MonoBehaviour
{
    void Awake()
    {
        Debug.unityLogger.logEnabled = true;
        Debug.Log("🔥 BootLogger Awake");

        string path = Path.Combine(Application.persistentDataPath, "manual_log.txt");
        File.WriteAllText(path, "✅ Manual log file created");
    }
}