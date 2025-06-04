using System;
using System.Numerics;
using EcsR3.Collections.Entities;
using EcsR3.Entities;
using EcsR3.Entities.Accessors;
using EcsR3.Extensions;
using EcsR3.MonoGame.Examples.Asteroids.Game.Blueprint;
using EcsR3.MonoGame.Examples.Asteroids.Game.Components;
using EcsR3.MonoGame.Examples.Asteroids.Game.Events;
using EcsR3.MonoGame.Examples.Asteroids.Types;
using EcsR3.Plugins.Transforms.Components;
using R3;
using SystemsR3.Plugins.Transforms.Models;
using SystemsR3.Scheduling;
using SystemsR3.Systems.Conventional;

namespace EcsR3.MonoGame.Examples.Asteroids.Game.Systems.Events;

public class MeteorHitEventSystem : IReactToEventSystem<MeteorCollidedWithProjectileEvent>
{
    public IEntityCollection EntityCollection { get; }
    public IUpdateScheduler UpdateScheduler { get; }
    public IEntityComponentAccessor EntityComponentAccessor { get; }
    public Random Random { get; } = new Random();

    public MeteorHitEventSystem(IEntityCollection entityCollection, IUpdateScheduler updateScheduler, IEntityComponentAccessor entityComponentAccessor)
    {
        UpdateScheduler = updateScheduler;
        EntityComponentAccessor = entityComponentAccessor;
        EntityCollection = entityCollection;
    }

    public Observable<MeteorCollidedWithProjectileEvent> ObserveOn(Observable<MeteorCollidedWithProjectileEvent> observable)
    { return observable; }

    public void Process(MeteorCollidedWithProjectileEvent eventData)
    {
        var meteorComponent = EntityComponentAccessor.GetComponent<MeteorComponent>(eventData.MeteorEntity);
        var projectileComponent = EntityComponentAccessor.GetComponent<ProjectileComponent>(eventData.ProjectileEntity);
        var playerComponent = EntityComponentAccessor.GetComponent<PlayerComponent>(projectileComponent.PlayerEntity);
        playerComponent.Score += CalculateScoreFor(meteorComponent);

        SpawnNewMeteorsIfNeeded(eventData.MeteorEntity);
       
        UpdateScheduler.OnPostUpdate.Take(1).Subscribe(_ =>
        {
            // The Lifecycle system may have removed these during the update cycle
            // so we need to confirm they still exist
            if(EntityCollection.Contains(eventData.MeteorEntity))
            { EntityCollection.Remove(eventData.MeteorEntity); }
            
            if(EntityCollection.Contains(eventData.ProjectileEntity))
            { EntityCollection.Remove(eventData.ProjectileEntity); }
        });
    }

    public void SpawnNewMeteorsIfNeeded(Entity meteorEntity)
    {
        var meteorComponent = EntityComponentAccessor.GetComponent<MeteorComponent>(meteorEntity);
        if (meteorComponent.Type == MeteorType.Small) { return; }

        var transformComponent = EntityComponentAccessor.GetComponent<Transform2DComponent>(meteorEntity);
        var parentTransform = transformComponent.Transform;

        var meteorBlueprint = new MeteorBlueprint() { MeteorType = meteorComponent.Type + 1 };
        var newMeteor1 = EntityCollection.Create(EntityComponentAccessor, meteorBlueprint);
        SetupChildTransforms(newMeteor1, parentTransform);
        
        var newMeteor2 = EntityCollection.Create(EntityComponentAccessor, meteorBlueprint);
        SetupChildTransforms(newMeteor2, parentTransform);
    }

    public void SetupChildTransforms(Entity childMeteor, Transform2D parentTransform)
    {
        var transformComponent = EntityComponentAccessor.GetComponent<Transform2DComponent>(childMeteor);
        var transform = transformComponent.Transform;

        var positionOffset = new Vector2(Random.Next(-64, 64));
        transform.Position = parentTransform.Position + positionOffset;
        var rotationOffset = Random.NextSingle();
        transform.Rotation = parentTransform.Rotation + rotationOffset;
    }

    private int CalculateScoreFor(MeteorComponent meteorComponent)
    {
        switch (meteorComponent.Type)
        {
            case MeteorType.Medium: return 200;
            case MeteorType.Small: return 500;
            case MeteorType.Big:
            default: return 100;
        }
    }
}