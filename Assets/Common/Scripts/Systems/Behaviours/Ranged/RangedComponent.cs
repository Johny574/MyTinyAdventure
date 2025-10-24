using System.Threading.Tasks;
using UnityEngine;

public class RangedComponent : Component
{
    LayerMask _targetLayer;
    ProjectileSO _projectile;
    StatpointsComponent _stats;

    public RangedComponent(RangedBehaviour behaviour, LayerMask targetLayer, ProjectileSO projectileSO) : base(behaviour)
    {
        _targetLayer = targetLayer;
        _projectile = projectileSO;
    }

    public void Initilize(StatpointsComponent stats)
    {
        _stats = stats;
    }

    public async void Fire(Vector2 direction, Vector2 origin) {
        var launchdata = new ProjectileLaunchData(_projectile, _targetLayer, direction, origin, _stats);
        ProjectileFactory.Instance.Launch(launchdata);
    }

    public void ChangeProjectile(ProjectileSO projectile) {
        _projectile = projectile;
    }
}