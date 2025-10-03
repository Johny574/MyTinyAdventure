using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExperienceBehaviour : MonoBehaviour
{
    public ExperienceComponent Experience { get; set; }
    [SerializeField] private List<Level> _levels = new();
    void Awake() {
        Experience = new(this, _levels);
    }
    void Start() {
        Experience.OnStart();
    }
}