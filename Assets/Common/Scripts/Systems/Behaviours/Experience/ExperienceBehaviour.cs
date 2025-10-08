using System.Collections.Generic;
using UnityEngine;

public class ExperienceBehaviour : MonoBehaviour
{
    public ExperienceComponent Experience { get; set; }
    [SerializeField] private List<Level> _levels = new();
    [SerializeField] float _xp = 0;
    void Awake() {
        Experience = new(this, _levels, _xp);
    }
    void Start() {
        Experience.Initilize(GetComponent<HealthBehaviour>().Health);
    }
}