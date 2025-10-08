using System;
using UnityEngine;

public class KillEnemiesQueststep : Queststep {
    public KillEnemiesQueststep(QueststepSO data, QuestingComponent parttaker, Quest quest) : base(data, parttaker, quest) {
    }

    // [NonSerialized] public ItemStack<int> Target;

    // public KillEnemiesQueststep(QueststepData data, EntityService parttaker, Quest quest, bool completed) : base(data, parttaker, quest, completed) {
    //     Target = (data as KillEnemyQueststepData).Target;
    //     (parttaker.Service<CacheService>() as PlayerCacheService).EnemyKilled += (enemy) => OnEnemyKilled(enemy.Behaviour);
    // }


    // // todo you can move enemy killed to the entity cache and subscribe to the event without the cast
    // public override void Complete() {
    //     (_parttaker.Service<CacheService>() as PlayerCacheService).EnemyKilled -= (enemy) => OnEnemyKilled(enemy.Behaviour);
    //     base.Complete();
    // }

    // private void OnEnemyKilled(EntityServiceBehaviour behaviour) {
    //     if (behaviour.ID != Target.Data) {
    //         return;
    //     }
    //     Target.Update(1);
    //     if (Target.Counter().Count >= Target.Counter().Limit) {
    //         Complete();
    //     }
    // }

    public override Vector2 Closestpoint(Vector2 origin) {
    //     var enemy = GlobalTracker.Instance.Tracker<EntityTracker>().GetClosestEntity(typeof(EnemyEntityServiceBehaviour), Target.Data, origin);
    //     if (enemy == null) {
    //         return Vector2.zero;
    //     }
    //     return enemy.transform.position;
        throw new NotImplementedException();
    }
}
