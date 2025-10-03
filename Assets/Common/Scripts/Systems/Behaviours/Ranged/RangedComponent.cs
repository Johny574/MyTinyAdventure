using System.Threading.Tasks;
using UnityEngine;

public class RangedComponent : Component
{
    LayerMask _targetLayer;
    ProjectileSO _projectile;
    
    public RangedComponent(RangedBehaviour behaviour, LayerMask targetLayer, ProjectileSO projectileSO) : base(behaviour) {
        _targetLayer = targetLayer;
        _projectile = projectileSO;
    }

    public async void Fire(Vector2 direction, Vector2 origin) {
        IPoolObject<ProjectileLaunchData> projectile = await ProjectileFactory.Instance.Pool.GetObject<ProjectileLaunchData>();
        projectile.Bind(new ProjectileLaunchData(_projectile, _targetLayer, direction, origin));
    }

    public void ChangeProjectile(ProjectileSO projectile) {
        _projectile = projectile;
    }
}