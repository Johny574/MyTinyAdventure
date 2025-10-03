using System;
using FletcherLibraries;

[Serializable]
public class TimeManager : Singleton<TimeManager> {
    public float TimeScale = 1f;
    public void Scale(float scale) => TimeScale = scale;
    
  
}