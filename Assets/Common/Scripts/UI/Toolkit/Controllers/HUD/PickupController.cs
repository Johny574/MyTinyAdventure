



using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class PickupController : MonoBehaviour
{
    List<PickupCommand> _currentPickup;
    Queue<PickupCommand> _pickupQueue;
    [SerializeField] VisualTreeAsset _slot_t;
    [SerializeField] UIDocument _document;

    float _displayTimer = 0f;
    void Awake() {
        _pickupQueue = new();
        _currentPickup = new();
    }

    void Start() {
        GameObject _player = GameObject.FindGameObjectWithTag("Player");
        _player.GetComponent<InventoryBehaviour>().Inventory.Added += (item, inventory) => Add(new PickupCommand(item, _slot_t, _document.rootVisualElement));
        // _player.GetComponent<InventoryBehaviour>().Inventory.Removed += (item, inventory) => Add(new PickupCommand(item, _slot_t, _document.rootVisualElement));
    }

    void Update() {
        if (_currentPickup.Count <= 0)
            return;

        if (_displayTimer < 2f)
            _displayTimer += Time.deltaTime;
        else  {
            Remove(_currentPickup[0]);
            _displayTimer = 0f;
        }
    }

    public void Add(PickupCommand pickupcommand) {
        if (_currentPickup.Count >= 5) {
            _pickupQueue.Enqueue(pickupcommand);
            return;
        }

        _currentPickup.Add(pickupcommand);
        pickupcommand.Execute();
    }

    void Remove(PickupCommand pickup) {
        pickup.Undo();
        _currentPickup.Remove(pickup);

        if (_pickupQueue.Count > 0)
            Add(_pickupQueue.Dequeue());
    }
}

public class PickupCommand : IUndoCommand
{
    VisualElement slot, _root;
    VisualTreeAsset _slot_t;
    ItemStack _item;

    public PickupCommand(ItemStack item, VisualTreeAsset slot_t, VisualElement root) {
        _slot_t = slot_t;
        _item = item;
        _root = root;
    }

    public void Execute() {
        float startX = 150;
        float finishX = 0;
        slot = _slot_t.CloneTree();
        slot.Q<Label>().text = $"{_item.Item.name} x{_item.Count}";
        slot.style.left = startX;
        var display = _root.Q<VisualElement>("PickupDisplay");
        display.Add(slot);

        DOTween.To(() => startX, x => {
            startX = x;
            slot.style.left= startX;
        }, finishX, .5f);
    }

    public void Undo() {
        float startX = 0;
        float finishX = 150;
        DOTween.To(() => startX, x => {
            startX = x;
            slot.style.left = startX;
        }, finishX, .5f).OnComplete(() => { slot.parent.Remove(slot); });
    }
}
