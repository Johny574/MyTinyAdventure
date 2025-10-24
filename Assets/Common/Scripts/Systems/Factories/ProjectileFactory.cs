

using System.Threading.Tasks;
using FletcherLibraries;
using UnityEngine;

public class ProjectileFactory: Factory<ProjectileFactory, ProjectileLaunchData> {

    public async void Launch(ProjectileLaunchData launchData )
    {
        IPoolObject<ProjectileLaunchData> obj = await GetObject(launchData);
        ((Projectile)obj).Launch(launchData);
    }
}
