using UnityEditor;
using UnityEngine;

public abstract class BuffSO : ScriptableObject {
    #if UNITY_EDITOR
        protected void OnValidate() {
            string path = AssetDatabase.GetAssetPath(this);
            GUID = AssetDatabase.AssetPathToGUID(path);
            EditorUtility.SetDirty(this);
        }
    #endif

    public string GUID;
    public float Duration = 5;
    public ParticleSystem Particles;
    public Sprite Icon;
    public AudioClip AddAudio;
}