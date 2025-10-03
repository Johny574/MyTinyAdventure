using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(ManaBehaviour))]
public class SkillsBehaviour : MonoBehaviour
{
    public SkillsComponent Skills { get; set; }
    [SerializeField] SkillSO[] _skills = new SkillSO[4];

    void Awake() {
        Skills = new(this, _skills.Select(x => x == null ? null : new Skill(gameObject, x)).ToArray());
    }

    void Start() {
        Skills.Initilize();
    }
    
    void Update() {
        Skills.Update();
    }

}