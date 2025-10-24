
using UnityEditor;
using UnityEngine;

public class Entity : MonoBehaviour, IUnique
{
    [SerializeField] Sprite _minimapMarkerSprite;
    public int UID { get => _id;  }
    [SerializeField] int _id;

#if UNITY_EDITOR
    void OnValidate()
    {
        if (_id != 0) return;
        _id = Mathf.Abs(System.Guid.NewGuid().GetHashCode());
        UnityEditor.EditorUtility.SetDirty(this);
    }
#endif

    protected void Awake() {
        SceneTracker.Instance.RegisterUnique<Entity>(gameObject, UID);
    }

    void Start() {
        MiniMapController.Instance.Register(this, _minimapMarkerSprite);
    }
}