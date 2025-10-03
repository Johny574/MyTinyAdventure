


using FletcherLibraries;
using UnityEngine;

public class CrosshairController : Singleton<CrosshairController>
{
    [SerializeField] Texture2D _cursor;
    Vector2 _hotspot;
    protected override void Awake() {
        base.Awake();
        Cursor.SetCursor(_cursor, _hotspot, CursorMode.ForceSoftware);
    }    
}