using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DialogueQueststep : Queststep {
    public DialogueQueststep(QueststepData data, QuestingComponent parttaker, Quest quest) : base(data, parttaker, quest) {
    }

    public override Vector2 Closestpoint(Vector2 origin) {
        var currentScene = SceneManager.GetActiveScene().name;
        if (Data.Scene != currentScene) {
            var path = Locations.SearchPath(currentScene, Data.Scene);
            return SceneTracker.Instance.Objects[typeof(TravelPoint)].Find(x => x.GetComponent<TravelPoint>().Destination.Equals(path)).transform.position;
        }

        return SceneTracker.Instance.Objects[typeof(NPC)].Find(x => x.GetComponent<NPC>().UID.Equals(((DialogueQueststepSO)Data).Target.GetComponent<NPC>().UID)).transform.position;
    }

}