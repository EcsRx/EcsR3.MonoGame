using System;
using EcsR3.Infrastructure;
using EcsR3.MonoGame.Modules;
using EcsR3.MonoGame.Wrappers;
using EcsR3.Plugins.Views;
using EcsR3.Plugins.Views.Extensions;
using SystemsR3.Extensions;
using SystemsR3.Infrastructure.Dependencies;
using SystemsR3.Infrastructure.Ninject;

namespace EcsR3.MonoGame.Application;

public abstract class EcsR3MonoGameApplication : EcsR3Application, IDisposable
{
    public override IDependencyRegistry DependencyRegistry { get; } = new NinjectDependencyRegistry();

    protected IEcsR3Game EcsR3Game { get; }
    protected IEcsR3ContentManager EcsR3ContentManager => EcsR3Game.EcsR3ContentManager;
    protected IEcsR3GraphicsDeviceManager DeviceManager => EcsR3Game.EcsR3GraphicsDeviceManager;

    public EcsR3MonoGameApplication()
    {
        EcsR3Game = new EcsR3Game();
        StartGame();
    }

    protected void StartGame()
    {
        BeforeGameStarted();
        EcsR3Game.GameLoading.SubscribeOnce(x => StartApplication());
        EcsR3Game.Run();
    }

    protected virtual void BeforeGameStarted()
    {}

    protected override void StartSystems()
    { this.StartAllBoundViewSystems(); }

    protected override void LoadPlugins()
    {
        RegisterPlugin(new ViewsPlugin());
    }

    protected override void LoadModules()
    {
        base.LoadModules();
        DependencyRegistry.LoadModule(new MonoGameModule(EcsR3Game));
    }
        
    public void Dispose()
    { EcsR3Game.Dispose(); }
}