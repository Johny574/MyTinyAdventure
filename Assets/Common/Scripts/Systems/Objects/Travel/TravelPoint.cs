using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class TravelPoint : MonoBehaviour, IInteractable, IUnique {
    [field:SerializeField] public string Destination { get; protected set; }

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

    [SerializeField] Vector2 _arrivePoint; 

    void OnEnable() {
        SceneTracker.Instance.RegisterUnique<TravelPoint>(gameObject, UID);   
    }

    public void CancelTarget() {
    }
    public void Target() {
    }

    public void Interact(GameObject accesor) {
        PlayerSaveData _playerSave = accesor.GetComponent<PlayerSave>().Save();
        _playerSave.CurrentScene = Destination;
        _playerSave.X = _arrivePoint.x;
        _playerSave.Y = _arrivePoint.y;
        Serializer.SaveFile(_playerSave, "Player.json", SaveSlot.AutoSave);
        GameManager.Instance.LoadSave(SaveSlot.AutoSave);
    }
}