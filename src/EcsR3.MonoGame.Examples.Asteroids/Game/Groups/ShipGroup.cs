using System;
using EcsR3.Groups;
using EcsR3.MonoGame.Components;
using EcsR3.MonoGame.Examples.Asteroids.Game.Components;
using EcsR3.Plugins.Transforms.Components;

namespace EcsR3.MonoGame.Examples.Asteroids.Game.Groups;

public class ShipGroup : IGroup
{
    public Type[] RequiredComponents { get; } = [typeof(PlayerComponent), typeof(ShootingComponent), typeof(ColliderComponent), typeof(HandlingComponent), typeof(MoveableComponent), typeof(SpriteComponent), typeof(Transform2DComponent)];
    public Type[] ExcludedComponents { get; } = Type.EmptyTypes;
}