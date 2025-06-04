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

    protected IEcsRxGame EcsRxGame { get; }
    protected IEcsRxContentManager EcsRxContentManager => EcsRxGame.EcsRxContentManager;
    protected IEcsRxGraphicsDeviceManager DeviceManager => EcsRxGame.EcsRxGraphicsDeviceManager;

    public EcsR3MonoGameApplication()
    {
        EcsRxGame = new EcsRxGame();
        StartGame();
    }

    protected void StartGame()
    {
        BeforeGameStarted();
        EcsRxGame.GameLoading.SubscribeOnce(x => StartApplication());
        EcsRxGame.Run();
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
        DependencyRegistry.LoadModule(new MonoGameModule(EcsRxGame));
    }
        
    public void Dispose()
    { EcsRxGame.Dispose(); }
}