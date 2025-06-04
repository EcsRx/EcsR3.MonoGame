using EcsR3.Collections.Entities;
using EcsR3.Entities.Accessors;
using EcsR3.Extensions;
using EcsR3.MonoGame.Examples.Asteroids.Game.Components;
using EcsR3.MonoGame.Examples.Asteroids.Game.Events;
using R3;
using SystemsR3.Scheduling;
using SystemsR3.Systems.Conventional;

namespace EcsR3.MonoGame.Examples.Asteroids.Game.Systems.Events;

public class ShipHitEventSystem : IReactToEventSystem<MeteorCollidedWithShipEvent>
{
    public IEntityCollection EntityCollection { get; }
    public IUpdateScheduler UpdateScheduler { get; }
    public IEntityComponentAccessor EntityComponentAccessor { get; }
    
    public ShipHitEventSystem(IEntityCollection entityCollection, IUpdateScheduler updateScheduler, IEntityComponentAccessor entityComponentAccessor)
    {
        UpdateScheduler = updateScheduler;
        EntityComponentAccessor = entityComponentAccessor;
        EntityCollection = entityCollection;
    }

    public Observable<MeteorCollidedWithShipEvent> ObserveOn(Observable<MeteorCollidedWithShipEvent> observable)
    { return observable; }

    public void Process(MeteorCollidedWithShipEvent eventData)
    {
        var playerComponent = EntityComponentAccessor.GetComponent<PlayerComponent>(eventData.ShipEntity);
        playerComponent.Score -= 500;

        if (playerComponent.Score < 0)
        { playerComponent.Score = 0; }
        
        UpdateScheduler.OnPostUpdate.Take(1).Subscribe(_ =>
        {
            EntityCollection.Remove(eventData.MeteorEntity);
        });
    }
}