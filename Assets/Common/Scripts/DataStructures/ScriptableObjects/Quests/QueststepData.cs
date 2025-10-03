using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public abstract class QueststepData : ScriptableObject {
    public float _xpReward = 50;
    
    #if UNITY_EDITOR
    void OnValidate() {
        string[] guid = AssetDatabase.FindAssets($"t:{typeof(QueststepData)}");
        
        for (int i = 0; i < guid.Length; i++) {
            string path = AssetDatabase.GUIDToAssetPath(guid[i]);
            QueststepData asset = AssetDatabase.LoadAssetAtPath<QueststepData>(path);
            asset.ID = i + 1;
            EditorUtility.SetDirty(asset);
        }    
    }
    #endif

    public string Scene;
    public string Description;
    [SerializeField] private int _id = 0;
    public int ID { get => _id; set => _id = value; }
    public List<Stack<ItemSO>> ItemRewards = new();
    public int CurrencyRewards;

    #if UNITY_INCLUDE_TESTS
    public void TestInit(int index) {
        _id = index;
        Description = ((char)('a' + index)).ToString();
    }
    #endif
}