using System;
using UnityEngine;

[Serializable]
public struct BuffData
{
    public float Timer;
    public string GUID;

    public BuffData(float timer, string gUID) {
        Timer = timer;
        GUID = gUID;
    }

    public void Tick() => Timer += Time.deltaTime;
}