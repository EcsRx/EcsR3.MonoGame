using System;
using System.Collections.Generic;
using EcsR3.Computeds.Entities.Registries;
using EcsR3.Entities;
using EcsR3.Entities.Accessors;
using EcsR3.MonoGame.Extensions;
using EcsR3.MonoGame.Rendering;
using EcsR3.MonoGame.Wrappers;
using SystemsR3.Attributes;
using SystemsR3.Executor.Handlers;
using SystemsR3.Extensions;
using SystemsR3.Scheduling;
using SystemsR3.Systems;
using R3;

namespace EcsR3.MonoGame.Systems.Handlers;

[Priority(6)]
public class SpriteBatchSystemHandler : IConventionalSystemHandler
{
    public readonly IComputedEntityGroupRegistry ComputedEntityGroupRegistry;       
    public readonly IDictionary<ISystem, IDisposable> SystemSubscriptions;
    public readonly IGameScheduler GameScheduler;
    public readonly IEcsRxGraphicsDevice GraphicsDevice;
    public readonly IRenderTarget2dRegistry RenderTarget2dRegistry;
    public readonly IEntityComponentAccessor EntityComponentAccessor;
        
    public SpriteBatchSystemHandler(IComputedEntityGroupRegistry computedEntityGroupRegistry, IEcsRxGraphicsDevice graphicsDevice, IRenderTarget2dRegistry renderTarget2dRegistry, IGameScheduler scheduler, IEntityComponentAccessor entityComponentAccessor)
    {
        ComputedEntityGroupRegistry = computedEntityGroupRegistry;
        SystemSubscriptions = new Dictionary<ISystem, IDisposable>();
        GraphicsDevice = graphicsDevice;
        GameScheduler = scheduler;
        EntityComponentAccessor = entityComponentAccessor;
        RenderTarget2dRegistry = renderTarget2dRegistry;
    }

    public bool CanHandleSystem(ISystem system)
    { return system.GetType().IsSubclassOf(typeof(SpriteBatchSystem)) ; }
       
    public void SetupSystem(ISystem system)
    {
        var castSystem = (SpriteBatchSystem)system;
        var computedEntityGroup = ComputedEntityGroupRegistry.GetComputedGroup(castSystem.Group);
        var renderTargetId = castSystem.GetRenderTexture2dId();
        var currentRenderTargets = GraphicsDevice.GetRenderTargets();

        var drawSubscription = GameScheduler.OnRender.Subscribe(elapsedTime =>
        {
            if (renderTargetId >= 0)
            {
                var renderTarget = RenderTarget2dRegistry.GetRenderTarget(renderTargetId);
                GraphicsDevice.SetRenderTarget(renderTarget);
            }
                
            castSystem.PreDraw();
                
            ExecuteForGroup(computedEntityGroup, castSystem);
                
            castSystem.PostDraw();

            if (renderTargetId >= 0)
            {
                GraphicsDevice.SetRenderTargets(currentRenderTargets);
            }
        });
            
        SystemSubscriptions.Add(system, drawSubscription);
    }

    private void ExecuteForGroup(IEnumerable<Entity> entities, SpriteBatchSystem castSystem)
    {
        foreach(var entity in entities)
        { castSystem.Process(EntityComponentAccessor, entity); }
    }

    public void DestroySystem(ISystem system)
    { SystemSubscriptions.RemoveAndDispose(system); }

    public void Dispose()
    {
        SystemSubscriptions.Values.DisposeAll();
        SystemSubscriptions.Clear();
    }
}