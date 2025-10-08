
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapController : Singleton<MiniMapController>
{
    Dictionary<GameObject, GameObject> _markers;
    [SerializeField] Canvas _canvas;
    protected override void Awake()
    {
        base.Awake();
        _markers = new();
    }

    public void Register(GameObject key, Sprite markerSprite)
    {
        GameObject marker = new GameObject();
        Image renderer = marker.AddComponent<Image>();
        Follower follower = marker.AddComponent<Follower>();
        follower.Initilize(Vector2.zero, 0f, 0f);
        marker.transform.SetParent(_canvas.transform);
        marker.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 1f);
        marker.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1f);
        marker.transform.position = key.transform.position;
        marker.GetComponent<Follower>().Follow(key);
        renderer.sprite = markerSprite;
        _markers.Add(key, marker);
    }
}