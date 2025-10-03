using System;
using UnityEngine;

[Serializable]
public class Currency
{
    [SerializeField] private int _gold;
    [SerializeField] private int _silver;
    [SerializeField] private int _copper;
    private int _totalCopper;
    public int TotalCopper { get => _totalCopper; }

    public Currency(int totalCopper) {
        _totalCopper = totalCopper;
        ConvertFromCopper();
    }

    public void Add(int total) {
        _totalCopper += total;
        ConvertFromCopper();
    }

    public void Remove(int total) {
        _totalCopper += total;
        ConvertFromCopper();
    }

    public void ConvertFromCopper() {
        _gold = _totalCopper / 10000;
        _silver = (_totalCopper % 10000) / 100;
        _copper = _totalCopper % 100;
    }

    #if UNITY_INCLUDE_TESTS
    public int Gold {
        get => _gold;
        set { _gold = value; }
    }

    public int Silver {
        get => _silver;
        set { _silver = value; }
    }

    public int Copper {
        get => _copper;
        set { _copper = value; }
    }
    
    #endif
}
