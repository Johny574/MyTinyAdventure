using System;
using System.Collections.Generic;
using UnityEngine;

public static class QuestFactory
{

    public static Dictionary<Type, Func<QueststepData, QuestingComponent, Quest, Queststep>> Queststeps = new() { { typeof(CollectItemQuestStepData),         (data, partaker, quest) => new CollectItemQueststep(data,  partaker, quest)}, { typeof(ReachpointQueststepData),          (data, partaker, quest) => new ReachpointQueststep(data,  partaker, quest)}, { typeof(KillEnemyQueststepData),           (data, partaker, quest) => new KillEnemiesQueststep(data,  partaker, quest)}, { typeof(DialogueQueststepSO),           (data, partaker, quest) => new DialogueQueststep(data,  partaker, quest)},
    };



}
