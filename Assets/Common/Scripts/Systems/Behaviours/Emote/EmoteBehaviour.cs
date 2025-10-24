using UnityEngine;

public class EmoteBehaviour : MonoBehaviour {

    public EmoteComponent Emotes { get; set; } 
    [SerializeField] bool _remove = true;
    [SerializeField] GameObject _emotePrefab;
    void Awake()
    {
        var emote = Instantiate(_emotePrefab).GetComponent<Emote>();
        emote.GetComponent<Follower>().Follow(gameObject);
        Emotes = new(this, emote, _remove);
    }

    
    void Start() {
        Emotes.Initilize(GetComponent<HealthBehaviour>());
    }

    void Update() {
        Emotes.Update();
    }
}