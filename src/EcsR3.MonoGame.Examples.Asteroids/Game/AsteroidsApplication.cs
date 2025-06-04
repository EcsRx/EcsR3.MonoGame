using EcsR3.Extensions;
using EcsR3.MonoGame.Application;
using EcsR3.MonoGame.Examples.Asteroids.Game.Blueprint;
using EcsR3.MonoGame.Examples.Asteroids.Game.Modules;
using EcsR3.Plugins.Transforms;
using SystemsR3.Infrastructure.Extensions;

namespace EcsR3.MonoGame.Examples.Asteroids.Game;

public class AsteroidsApplication : EcsR3MonoGameApplication
{
    protected override void LoadModules()
    {
        base.LoadModules();
        DependencyRegistry.LoadModule<GameModule>();
    }

    protected override void LoadPlugins()
    {
        base.LoadPlugins();
        RegisterPlugin(new TransformsPlugin());
    }

    protected override void ApplicationStarted()
    {
        EntityCollection.Create<ShipBlueprint>(EntityComponentAccessor);
    }
}