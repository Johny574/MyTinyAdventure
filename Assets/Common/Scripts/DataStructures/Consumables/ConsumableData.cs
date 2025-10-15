


using System;

[Serializable]
public class ConsumableData
{
    public string GUID;
    public int Counter;
    public float Timer;

    public ConsumableData(string gUID, int counter, float timer) {
        GUID = gUID;
        Counter = counter;
        Timer = timer;
    }
}