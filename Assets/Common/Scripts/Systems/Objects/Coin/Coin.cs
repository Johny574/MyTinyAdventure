using UnityEngine;


[RequireComponent(typeof(Animator))]
public class Coin : MonoBehaviour, ICollectable, IPoolObject<int> {
    [SerializeField] private int _amount;
    [SerializeField] private AnimatorOverrideController[] _coinSprites;
    [SerializeField] private Animator _animator;
    
    public void Bind(int variant) {
        if (_amount > 10000)
            _animator.runtimeAnimatorController = _coinSprites[0];
        else if (_amount > 100)
            _animator.runtimeAnimatorController = _coinSprites[1];
        else
            _animator.runtimeAnimatorController = _coinSprites[2];
    }
    
    void OnEnable() {
        SceneTracker.Instance.Register<Coin>(gameObject);   
    }

    void OnDisable() {
        SceneTracker.Instance?.Unregister<Coin>(gameObject);   
    }

    public void Collect(GameObject collector) {
        collector.GetComponent<CurrencyBehaviour>().Currency.Add(_amount);
        gameObject.SetActive(false);
    }
}