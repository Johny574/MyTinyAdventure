
using UnityEditor;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] Sprite _minimapMarkerSprite;
    public int UID;

#if UNITY_EDITOR
    void OnValidate()
    {
        string[] guid = AssetDatabase.FindAssets($"t:{typeof(Entity)}");

        for (int i = 0; i < guid.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid[i]);
            Entity asset = AssetDatabase.LoadAssetAtPath<Entity>(path);
            asset.UID = i + 1;
            EditorUtility.SetDirty(asset);
        }
    }
#endif

    protected void Awake() {
        SceneTracker.Instance.Register<Entity>(gameObject);
    }

    void Start() {
        MiniMapController.Instance.Register(gameObject, _minimapMarkerSprite);
    }


    protected void OnDisable()
    {
        SceneTracker.Instance?.Unregister<Entity>(gameObject);
    }
}