using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DialogueQueststep : Queststep {
    public DialogueQueststep(QueststepSO data, QuestingComponent parttaker, Quest quest) : base(data, parttaker, quest) {
    }

    public override Vector2 Closestpoint(Vector2 origin) {
        var currentScene = SceneManager.GetActiveScene().name;
        if (SO.Scene != currentScene) {
            var path = Locations.SearchPath(currentScene, SO.Scene);
            return SceneTracker.Instance.Objects[typeof(TravelPoint)].Find(x => x.GetComponent<TravelPoint>().Destination.Equals(path)).transform.position;
        }

        return SceneTracker.Instance.Objects[typeof(Entity)].Find(x => x.GetComponent<Entity>().UID.Equals(((DialogueQueststepSO)SO).Target.GetComponent<Entity>().UID)).transform.position;
    }

}