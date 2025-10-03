using System;

[Serializable]
public class BarData {
    public float Amount;
    public int Max;
    public float Fill;
    public string Display;
    
    public BarData(float amount, int max) {
        Amount = amount;
        Max = max;
        CalculateFill();
    }

    public void CalculateFill() {
        Fill = Amount / Max * 100;
        Display = $"{Amount}/{Max}";
    }
}