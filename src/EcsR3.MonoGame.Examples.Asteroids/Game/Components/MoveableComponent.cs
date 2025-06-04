using System.Numerics;
using EcsR3.Components;

namespace EcsR3.MonoGame.Examples.Asteroids.Game.Components;

public class MoveableComponent : IComponent
{
    public Vector2 MovementChange { get; set; }
    public float DirectionChange { get; set; }
}