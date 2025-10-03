using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] bool _persistant = true;
    public static T Instance;
    protected virtual void Awake() {
        if (Instance == null)
            Instance = CreateInstance();
    }

    T CreateInstance() {
        T instance = (T)FindFirstObjectByType(typeof(T));

        if (instance == null) {
            GameObject singleton = new GameObject();
            instance = singleton.AddComponent<T>();
        }

        if (_persistant)
            DontDestroyOnLoad(instance.gameObject);

        return instance;
    }

    void OnDestroy() {
        if (!_persistant)
            Instance = null;
    }
}