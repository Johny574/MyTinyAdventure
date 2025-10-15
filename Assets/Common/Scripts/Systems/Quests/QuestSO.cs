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
    [field: SerializeField] public List<QueststepSO> Steps { get; set; }
    [NonSerialized] public string[] StartDialogue = new string[2]{"test", "test"};
    public string Tag, Title, Description;
    public List<SceneObjects> DisabledObjects = new(); // List of UIDs that get disabled when step is completed
    public List<SceneObjects> EnabledObjects = new(); // List of UIDs that get enabled when step is completed
}