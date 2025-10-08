using UnityEngine;

public class CurrencyBehaviour : MonoBehaviour {
    public CurrencyComponent Currency { get; set; }
    [SerializeField] int _amount = 0;
    void Awake() {
        Currency = new(this, _amount);
    }
}