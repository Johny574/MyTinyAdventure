
using UnityEditor;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] Sprite _minimapMarkerSprite;
    public int UID;

#if UNITY_EDITOR
    void OnValidate()
    {
        if (UID != 0) return;
        UID = Mathf.Abs(System.Guid.NewGuid().GetHashCode());
        UnityEditor.EditorUtility.SetDirty(this);
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