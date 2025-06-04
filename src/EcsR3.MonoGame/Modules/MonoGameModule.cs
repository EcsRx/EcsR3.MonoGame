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
    private readonly IEcsR3Game _ecsR3Game;
        
    public MonoGameModule(IEcsR3Game ecsR3Game)
    { _ecsR3Game = ecsR3Game; }

    public void Setup(IDependencyRegistry registry)
    {
        registry.Bind<IEcsR3Game>(x => x.ToInstance(_ecsR3Game));

        registry.Unbind<IUpdateScheduler>();
        registry.Bind<IUpdateScheduler>(x => x.ToInstance(_ecsR3Game));
        registry.Bind<IGameScheduler>(x => x.ToInstance(_ecsR3Game));
            
        registry.Bind<IEcsR3ContentManager>(x => x.ToInstance(_ecsR3Game.EcsR3ContentManager));
        registry.Bind<IEcsR3SpriteBatch>(x => x.ToInstance(_ecsR3Game.EcsR3SpriteBatch));
        registry.Bind<IEcsR3GraphicsDeviceManager>(x => x.ToInstance(_ecsR3Game.EcsR3GraphicsDeviceManager));
        registry.Bind<IEcsR3GraphicsDevice>(x => x.ToInstance(_ecsR3Game.EcsR3GraphicsDevice));

        registry.Bind<IRenderTarget2dRegistry, RenderTarget2dRegistry>();
        registry.Bind<IRenderTargetCubeRegistry, RenderTargetCubeRegistry>();
            
        registry.Bind<IConventionalSystemHandler, SpriteBatchSystemHandler>();
    }
}