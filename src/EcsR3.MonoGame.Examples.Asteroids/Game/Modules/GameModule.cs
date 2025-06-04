using Comora;
using EcsR3.Components.Database;
using EcsR3.Infrastructure.Extensions;
using EcsR3.MonoGame.Examples.Asteroids.Game.Components;
using EcsR3.MonoGame.Examples.Asteroids.Game.Computed;
using EcsR3.MonoGame.Wrappers;
using EcsR3.Plugins.Transforms.Components;
using SystemsR3.Infrastructure.Dependencies;
using SystemsR3.Infrastructure.Extensions;

namespace EcsR3.MonoGame.Examples.Asteroids.Game.Modules;

public class GameModule : IDependencyModule
{
    public void Setup(IDependencyRegistry registry)
    {
        registry.Bind<ICamera>(x => x.ToMethod(resolver =>
        {
            var ecsR3GraphicsDevice = resolver.Resolve<IEcsR3GraphicsDevice>();
            return new Camera(ecsR3GraphicsDevice.InternalDevice);
        }));

        registry.Bind<ComputedRuntimeColliders>(x => x.ToMethod(resolver =>
        {
            var computedDatabase = resolver.Resolve<IComponentDatabase>();
            var computedComponentGroup =
                resolver.ResolveComputedComponentGroup<ColliderComponent, Transform2DComponent>();
            return new ComputedRuntimeColliders(computedDatabase, computedComponentGroup);
        }));
    }
}