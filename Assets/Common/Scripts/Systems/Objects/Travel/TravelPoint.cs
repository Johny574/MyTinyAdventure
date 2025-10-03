using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class TravelPoint : MonoBehaviour, IInteractable {
    [field:SerializeField] public string Destination { get; protected set; }
    [SerializeField] Vector2 _arrivePoint; 

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
        _playerSave.CurrentScene = Locations.Index(Destination);
        _playerSave.X = _arrivePoint.x;
        _playerSave.Y = _arrivePoint.y;
        Serializer.SaveFile(_playerSave, "Player.json", SaveSlot.AutoSave);
        GameManager.Instance.LoadSave(SaveSlot.AutoSave);
    }
}