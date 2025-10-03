using UnityEngine;

public class PortalBehaviour : MonoBehaviour  {
     GameObject portal = null;
    [SerializeField] GameObject _portalPrefab;
    public void OpenPortal(string destination) {
        if (portal != null) {
            // todo : probably make like a animation
            GameObject.Destroy(portal);
            portal = null;
        }

        portal = GameObject.Instantiate(_portalPrefab);
        Debug.Log(portal.gameObject);
        // portal.transform.position = (Vector2)Entity.Component<Transform>().position + (Entity.Service<AimService>().Flipped ? Vector2.left : Vector2.right);
        portal.GetComponent<Portal>().Initilize(destination);
        portal.gameObject.SetActive(true);
        portal.GetComponent<Animator>().SetBool("Open", true);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            // OpenPortal(CentralManager.Instance.Manager<LocationManager>().GetLastTown(CurrentScene));
        }
    }
}