using EcsR3.MonoGame.Rendering;
using EcsR3.MonoGame.Systems.Handlers;
using EcsR3.MonoGame.Wrappers;
using SystemsR3.Executor.Handlers;
using SystemsR3.Infrastructure.Dependencies;
using SystemsR3.Infrastructure.Extensions;
using SystemsR3.Scheduling;

namespace EcsR3.MonoGame.Modules;

public class MonoGameModule : IDependencyModule
{
    private readonly IEcsRxGame _ecsRxGame;
        
    public MonoGameModule(IEcsRxGame ecsRxGame)
    { _ecsRxGame = ecsRxGame; }

    public void Setup(IDependencyRegistry registry)
    {
        registry.Bind<IEcsRxGame>(x => x.ToInstance(_ecsRxGame));

        registry.Unbind<IUpdateScheduler>();
        registry.Bind<IUpdateScheduler>(x => x.ToInstance(_ecsRxGame));
        registry.Bind<IGameScheduler>(x => x.ToInstance(_ecsRxGame));
            
        registry.Bind<IEcsRxContentManager>(x => x.ToInstance(_ecsRxGame.EcsRxContentManager));
        registry.Bind<IEcsRxSpriteBatch>(x => x.ToInstance(_ecsRxGame.EcsRxSpriteBatch));
        registry.Bind<IEcsRxGraphicsDeviceManager>(x => x.ToInstance(_ecsRxGame.EcsRxGraphicsDeviceManager));
        registry.Bind<IEcsRxGraphicsDevice>(x => x.ToInstance(_ecsRxGame.EcsRxGraphicsDevice));

        registry.Bind<IRenderTarget2dRegistry, RenderTarget2dRegistry>();
        registry.Bind<IRenderTargetCubeRegistry, RenderTargetCubeRegistry>();
            
        registry.Bind<IConventionalSystemHandler, SpriteBatchSystemHandler>();
    }
}