using EcsR3.Collections.Entities;
using EcsR3.Entities;
using EcsR3.Entities.Accessors;
using EcsR3.Extensions;
using EcsR3.Groups;
using EcsR3.MonoGame.Examples.Asteroids.Game.Components;
using EcsR3.Systems;
using R3;
using SystemsR3.Extensions;
using SystemsR3.Scheduling;

namespace EcsR3.MonoGame.Examples.Asteroids.Game.Systems.Game;

public class LifetimeClearingSystem : IBasicEntitySystem
{
    public IGroup Group { get; } = new Group(typeof(HasLifetimeComponent));
    public IEntityCollection EntityCollection { get; }
    public IUpdateScheduler UpdateScheduler { get; }

    public LifetimeClearingSystem(IEntityCollection entityCollection, IUpdateScheduler updateScheduler)
    {
        UpdateScheduler = updateScheduler;
        EntityCollection = entityCollection;
    }

    public void Process(IEntityComponentAccessor entityComponentAccessor, Entity entity, ElapsedTime elapsedTime)
    {
        var lifetimeComponent = entityComponentAccessor.GetComponent<HasLifetimeComponent>(entity);
        lifetimeComponent.AliveTime += (float)elapsedTime.DeltaTime.TotalSeconds;

        if (lifetimeComponent.AliveTime >= lifetimeComponent.MaxAliveTime)
        {
            UpdateScheduler.OnPostUpdate.SubscribeOnce(_ => {
                EntityCollection.Remove(entity);
            });
        }
    }
}