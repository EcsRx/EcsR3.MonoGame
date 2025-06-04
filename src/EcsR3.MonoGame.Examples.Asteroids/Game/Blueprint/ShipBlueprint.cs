using EcsR3.Blueprints;
using EcsR3.Entities;
using EcsR3.Entities.Accessors;
using EcsR3.Extensions;
using EcsR3.MonoGame.Components;
using EcsR3.MonoGame.Examples.Asteroids.Game.Components;
using EcsR3.Plugins.Transforms.Components;

namespace EcsR3.MonoGame.Examples.Asteroids.Game.Blueprint;

public class ShipBlueprint : IBlueprint
{
    public void Apply(IEntityComponentAccessor entityComponentAccessor, Entity entity)
    {
        var handlingComponent = new HandlingComponent()
        {
            MovementSpeed = 100f,
            RotationSpeed = 5.0f
        };
            
        entityComponentAccessor.AddComponents(entity, new PlayerComponent(), handlingComponent, new ColliderComponent(), 
            new ShootingComponent(), new MoveableComponent(), new SpriteComponent(), new Transform2DComponent());
    }
}