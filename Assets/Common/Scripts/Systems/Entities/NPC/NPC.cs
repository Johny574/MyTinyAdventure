



using UnityEditor;
using UnityEngine;

public class NPC : MonoBehaviour {
    [SerializeField] Sprite _minimapMarker;
    public int UID;
    #if UNITY_EDITOR
    void OnValidate() {
        string[] guid = AssetDatabase.FindAssets($"t:{typeof(NPC)}");
        
        for (int i = 0; i < guid.Length; i++) {
            string path = AssetDatabase.GUIDToAssetPath(guid[i]);
            NPC asset = AssetDatabase.LoadAssetAtPath<NPC>(path);
            asset.UID = i + 1;
            EditorUtility.SetDirty(asset);
        }    
    }
    #endif
    void Awake() {
        SceneTracker.Instance.Register<NPC>(gameObject);
        // MiniMapController.Instance.Register(gameObject, _minimapMarker);
    }

    void OnDisable() {
        SceneTracker.Instance?.Unregister<NPC >(gameObject);   
    }

}