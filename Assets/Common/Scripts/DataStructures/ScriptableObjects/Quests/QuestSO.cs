using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "_quest", menuName = "Quests/Quest", order = 1)]
public class QuestSO : ScriptableObject {

    #if UNITY_EDITOR
        protected void OnValidate() {
            string path = AssetDatabase.GetAssetPath(this);
            GUID = AssetDatabase.AssetPathToGUID(path);
            EditorUtility.SetDirty(this);
        }
    #endif
    public string GUID;
    [field: SerializeField] public List<QueststepData> Steps { get; set; }
    [NonSerialized] public List<ItemStack> ItemRewards = new();
    [NonSerialized] public int CurrencyRewards;
    [NonSerialized] public string[] StartDialogue = new string[2]{"test", "test"};
    public string Tag, Title, Description;
}