using System;
using TMPro;
using UnityEngine;

public class Bar : MonoBehaviour, IPoolObject<BarData> {
    [SerializeField] TextMeshProUGUI _textField;
    [SerializeField] SlicedFilledImage _fill;
    [SerializeField] bool _reverse = false;

    #if UNITY_EDITOR
    void OnValidate() {
        if (_fill == null) 
            _fill = GetComponentInChildren<SlicedFilledImage>();
    }
    #endif

    public void Bind(BarData variant) {
        _fill.fillAmount = variant.Amount / variant.Max;

        if (_textField == null) 
            return;

        _textField.text = $"{variant.Amount}/{variant.Max}";
    }
}