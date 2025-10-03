


using UnityEngine;

[RequireComponent(typeof(ExperienceBehaviour))]
[RequireComponent(typeof(GearBehaviour))]
public class StatpointsBehaviour : MonoBehaviour
{
    public StatpointsComponent Stats { get; set; }
    [SerializeField] StatPoints defualtStats;

    void Awake() {
        Stats = new(this, defualtStats, GetComponent<ExperienceBehaviour>().Experience);
    }

    void Start() {
        Stats.Initilize(GetComponent<GearBehaviour>().Gear);
    }
}