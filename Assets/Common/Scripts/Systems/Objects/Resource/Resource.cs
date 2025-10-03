using UnityEngine;

public class Resource : MonoBehaviour  {
    SpriteRenderer _renderer;
    [SerializeField] ItemComponent _item;

    void Start() {
        _renderer = GetComponent<SpriteRenderer>();   
        SceneTracker.Instance.Register<Resource>(gameObject);   
    }

    void OnDisable() {
        // SceneTracker.Instance?.Unregister<Resource>(gameObject);   
    }

    // void Start() {
    //     Bind(_item._stack);
    // }

    // public void Bind<T>(T variant) {
    //     _item.Initilize(variant);
    // }

    // public void Collect(EntityService collector) {
    //     _item.Interact(collector);
    //     gameObject.SetActive(false);
        
    // }
}