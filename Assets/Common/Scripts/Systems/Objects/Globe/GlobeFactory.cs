




using System;
using System.Collections.Generic;
using UnityEngine;

public static class GlobeFactory {
    public static Dictionary<GlobeComponent.Type, Func<GlobeBehaviour, GlobeComponent>> Globes => new() {  {GlobeComponent.Type.Health, (behaviour) => new HealthGlobeComponent(behaviour)}, {GlobeComponent.Type.Experience, (behaviour) => new ExperienceGlobeComponent(behaviour)}
    };
}