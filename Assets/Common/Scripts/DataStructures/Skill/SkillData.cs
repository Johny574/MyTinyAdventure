

using System;

[Serializable]
public struct SkillData
{
    public string GUID;
    public float Timer;

    public SkillData(string gUID, float timer) {
        GUID = gUID;
        Timer = timer;
    }
}