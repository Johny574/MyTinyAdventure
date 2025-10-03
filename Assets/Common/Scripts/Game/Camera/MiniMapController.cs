
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapController : Singleton<MiniMapController>
{
    Dictionary<GameObject, GameObject> _markers;
    [SerializeField] Canvas _canvas;
    protected override  void Awake() {
        base.Awake();
        _markers = new();
    }

    public void Register(GameObject key, Sprite marker) {
        GameObject obj = new GameObject();
        Image renderer = obj.AddComponent<Image>();
        obj.transform.SetParent(_canvas.transform);
        obj.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 1f);
        obj.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1f);
        obj.transform.position = key.transform.position;
        renderer.sprite = marker;
        _markers.Add(key, obj);
    }
}