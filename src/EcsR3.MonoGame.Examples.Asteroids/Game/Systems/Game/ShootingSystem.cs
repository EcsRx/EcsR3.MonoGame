using System.Numerics;
using EcsR3.Collections.Entities;
using EcsR3.Entities;
using EcsR3.Entities.Accessors;
using EcsR3.Extensions;
using EcsR3.Groups;
using EcsR3.MonoGame.Examples.Asteroids.Extensions;
using EcsR3.MonoGame.Examples.Asteroids.Game.Blueprint;
using EcsR3.MonoGame.Examples.Asteroids.Game.Components;
using EcsR3.Plugins.Transforms.Components;
using EcsR3.Systems;
using SystemsR3.Plugins.Transforms.Extensions;
using SystemsR3.Scheduling;

namespace EcsR3.MonoGame.Examples.Asteroids.Game.Systems.Game;

public class ShootingSystem : IBasicEntitySystem
{
    public float FireRateDelay = 0.5f;
    
    public IGroup Group { get; } = new Group(typeof(ShootingComponent), typeof(Transform2DComponent));
    
    public IEntityCollection EntityCollection { get; }

    public ShootingSystem(IEntityCollection entityCollection)
    { EntityCollection = entityCollection; }

    public void Process(IEntityComponentAccessor entityComponentAccessor, Entity entity, ElapsedTime elapsedTime)
    {
        var shootingComponent = entityComponentAccessor.GetComponent<ShootingComponent>(entity);
        if (shootingComponent.FireTimeLeft > 0)
        { shootingComponent.FireTimeLeft -= (float)elapsedTime.DeltaTime.TotalSeconds; }

        if (shootingComponent.IsFiring && shootingComponent.FireTimeLeft <= 0)
        {
            shootingComponent.FireTimeLeft += FireRateDelay;
            CreateShotFor(entityComponentAccessor, entity);
        }
    }

    public void CreateShotFor(IEntityComponentAccessor entityComponentAccessor, Entity shooterEntity)
    {
        var shooterTransformComponent = entityComponentAccessor.GetComponent<Transform2DComponent>(shooterEntity);
        var shooterTransform = shooterTransformComponent.Transform;

        var projectileEntity = EntityCollection.Create<ProjectileBlueprint>(entityComponentAccessor);
        var projectileTransformComponent = entityComponentAccessor.GetComponent<Transform2DComponent>(projectileEntity);
        var projectileTransform = projectileTransformComponent.Transform;
        projectileTransform.Position = shooterTransform.Position;
        projectileTransform.Rotation = shooterTransform.Rotation;
        projectileTransform.Position += projectileTransform.MGForward() * 64.0f;

        var projectileComponent = entityComponentAccessor.GetComponent<ProjectileComponent>(projectileEntity);
        projectileComponent.PlayerEntity = shooterEntity;
        
        var moveableComponent = entityComponentAccessor.GetComponent<MoveableComponent>(projectileEntity);
        moveableComponent.MovementChange = new Vector2(0, 1); // No Strafe, Just Forwards
    }
}