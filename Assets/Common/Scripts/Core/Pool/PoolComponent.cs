using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.Exceptions;

public class PoolBehaviour : MonoBehaviour
{
    AsyncOperationHandle<GameObject>? _loadedReference = null;
    public Queue<GameObject> _availableObjects { get; set; } = new();
    List<GameObject> _objects = new();
    protected int _size = 16;
    [SerializeField] protected AssetReference _prefab;

    public async void Awake() {
        await Addressables.InitializeAsync().Task;
        CreatePool();
    }

    async void CreatePool() {
        if (_size == 0)
            return;

        else if (_prefab == null)
            throw new System.Exception($"No prefab assigned to pool {this}.");

        for (int i = 0; i < _size; i++) {
            GameObject obj = await InstantiateByReference(_prefab, new SpawnData(Vector3.zero, Vector3.zero, Vector3.one, false, transform));
            _objects.Add(obj);
            _availableObjects.Enqueue(obj);
        }
    }

    public async Task<IPoolObject<T>> GetObject<T>() {
        GameObject obj;
        try {
            obj = _availableObjects.Dequeue();
        }
        catch {
            obj = await InstantiateByReference(_prefab, new SpawnData(Vector3.zero, Vector3.zero, Vector3.one, false, transform));
        }

        obj.gameObject.SetActive(true);

        return obj.GetComponent<IPoolObject<T>>();
    }

    async Task<AsyncOperationHandle<T>> LoadAsset<T>(object key) {
        try {
            AsyncOperationHandle<T> loadedasset = Addressables.LoadAssetAsync<T>(key);
            await loadedasset.Task;

            if (loadedasset.Status == AsyncOperationStatus.Succeeded) {
                return loadedasset;
            }
            else {
                Debug.LogError("Asset loading failed.");
                return default;
            }
        }

        catch (OperationException oe) {
            Debug.Log(oe);
            return default;
        }
    }

     async Task<GameObject> InstantiateByReference(AssetReference reference, SpawnData transform) {
        if (!reference.RuntimeKeyIsValid()) {
            Debug.Log($"Invalid runtime key on asset {reference}: {reference.RuntimeKey}");
            return null;
        }

        return await Instatiate(reference, transform);
    }

     async Task<GameObject> Instatiate(AssetReference key, SpawnData transform) {
        if (_loadedReference == null) {
            _loadedReference = await LoadAsset<GameObject>(key);
        }

        GameObject newObject = Object.Instantiate(((AsyncOperationHandle<GameObject>)_loadedReference).Result);
        PoolObject poolObject = newObject.AddComponent<PoolObject>();
        poolObject.Disable += (poolobj) => _availableObjects.Enqueue(poolobj);
        transform.SetTransform(newObject.transform);
        return newObject;
    }

    public void OnDisable() {
        foreach (var obj in _objects) {
            GameObject.Destroy(obj);
        }
    }
}