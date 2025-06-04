using System;
using EcsR3.Collections.Entities;
using EcsR3.Entities.Accessors;
using EcsR3.Extensions;
using EcsR3.MonoGame.Examples.Asteroids.Game.Blueprint;
using R3;
using SystemsR3.Systems.Conventional;

namespace EcsR3.MonoGame.Examples.Asteroids.Game.Systems.Game;

public class MeteorSpawningSystem : IReactiveSystem<Unit>
{
    public IEntityCollection EntityCollection { get; }
    public IEntityComponentAccessor EntityComponentAccessor { get; }
    
    private double _elapsedTime;

    public MeteorSpawningSystem(IEntityCollection entityCollection, IEntityComponentAccessor entityComponentAccessor)
    {
        EntityCollection = entityCollection;
        EntityComponentAccessor = entityComponentAccessor;
    }

    public Observable<Unit> ReactTo() => Observable.Interval(TimeSpan.FromSeconds(2.0f));

    public void Execute(Unit _)
    {
        EntityCollection.Create<MeteorBlueprint>(EntityComponentAccessor);
    }
}