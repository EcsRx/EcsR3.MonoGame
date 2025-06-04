using EcsR3.Components;
using EcsR3.MonoGame.Examples.Asteroids.Types;

namespace EcsR3.MonoGame.Examples.Asteroids.Game.Components;

public class MeteorComponent : IComponent
{
    public MeteorType Type { get; set; } = MeteorType.Big;
}