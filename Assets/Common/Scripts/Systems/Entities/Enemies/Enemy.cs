


using UnityEngine;

public class Enemy : MonoBehaviour, IPoolObject<Enemy>
{
    [SerializeField] Sprite _minimapMarker;
    void Start() {
        SceneTracker.Instance.Register<Enemy>(gameObject);
        MiniMapController.Instance.Register(gameObject, _minimapMarker);
    }

    void OnDisable() {
        SceneTracker.Instance?.Unregister<Enemy>(gameObject);
    }

    public void Bind(Enemy variant) {
        bool melee = variant.GetComponent<MeleeEnemyStatemachine>().enabled;
        bool ranged = variant.GetComponent<MeleeEnemyStatemachine>().enabled;
        bool magic = variant.GetComponent<MeleeEnemyStatemachine>().enabled;

        GetComponent<MeleeEnemyStatemachine>().enabled = melee;
        GetComponent<MeleeBehaviour>().enabled = melee;

        // GetComponent<RangedEnemyStatemachine>().enabled = melee;
        // GetComponent<MeleeBehaviour>().enabled = melee;

        // GetComponent<MeleeEnemyStatemachine>().enabled = melee;
        // GetComponent<MeleeBehaviour>().enabled = melee;


    }
}