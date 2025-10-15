using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class TravelPoint : MonoBehaviour, IInteractable {
    [field:SerializeField] public string Destination { get; protected set; }
    [SerializeField] Vector2 _arrivePoint; 

    public int UID;

    #if UNITY_EDITOR
        void OnValidate()
        {
            if (UID != 0) return;
            UID = Mathf.Abs(System.Guid.NewGuid().GetHashCode());
            UnityEditor.EditorUtility.SetDirty(this);
        }
    #endif

    void Awake() {
        SceneTracker.Instance.Register<TravelPoint>(gameObject);   
    }

    void OnDisable() {
        SceneTracker.Instance?.Unregister<TravelPoint>(gameObject);   
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