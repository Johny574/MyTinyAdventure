using UnityEngine;

public class Portal : TravelPoint, IDynamic {
    [SerializeField] Animator _animator;
    
    public void Initilize(string destination) {
        Destination = destination;
    }

    public void Open() {
        _animator.SetBool("Open", false);
    }

    public void Close() {
        _animator.SetBool("Close", false);
    }

    public void Toggle() {
    }
}