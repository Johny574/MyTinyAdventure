using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public class SkillSO : ScriptableObject {
    #if UNITY_EDITOR
        protected void OnValidate() {
            string path = AssetDatabase.GetAssetPath(this);
            GUID = AssetDatabase.AssetPathToGUID(path);
            EditorUtility.SetDirty(this);
        }
    #endif

    public string GUID;
    public float CooldownDuration = 5f;
    public int ManaCost = 5;
    [field: SerializeField] public AudioClip CastAudio { get; private set; }
    public Sprite Icon;
}