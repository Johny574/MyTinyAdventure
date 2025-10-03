

using UnityEngine;

public class LocationBehaviour : MonoBehaviour
{
    public LocationComponent Location { get; set; }
    void Awake() => Location = new(this);
    void Start() => Location.Initilize();
}