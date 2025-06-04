using EcsR3.Components;
using EcsR3.Entities;

namespace EcsR3.MonoGame.Examples.Asteroids.Game.Components;

public class ProjectileComponent : IComponent
{
    public Entity PlayerEntity { get; set; }
}