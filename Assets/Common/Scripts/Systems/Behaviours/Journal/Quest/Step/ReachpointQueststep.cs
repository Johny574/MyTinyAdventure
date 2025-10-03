
using UnityEngine;

public class ReachpointQueststep : Queststep {
    
    [SerializeField] private float _distance = .5f;
    public ReachpointQueststep(QueststepData data, QuestingComponent parttaker, Quest quest) : base(data, parttaker, quest) {
    }

    public override Vector2 Closestpoint(Vector2 origin) => ((ReachpointQueststepData)Data).Position;
    public void Tick(Vector2 point) {
        if (Vector2.Distance(point, ((ReachpointQueststepData)Data).Position) < _distance) {
            if (point == ((ReachpointQueststepData)Data).Position) {
                Complete();
            }
        }
    }
}