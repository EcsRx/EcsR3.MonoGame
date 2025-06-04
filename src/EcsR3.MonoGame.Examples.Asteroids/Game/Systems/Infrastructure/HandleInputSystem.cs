using System.Numerics;
using EcsR3.Entities;
using EcsR3.Entities.Accessors;
using EcsR3.Extensions;
using EcsR3.Groups;
using EcsR3.MonoGame.Examples.Asteroids.Game.Components;
using EcsR3.Systems;
using Microsoft.Xna.Framework.Input;
using SystemsR3.Attributes;
using SystemsR3.Scheduling;
using SystemsR3.Types;

namespace EcsR3.MonoGame.Examples.Asteroids.Game.Systems.Infrastructure;

[Priority(PriorityTypes.SuperHigh)]
public class HandleInputSystem : IBasicEntitySystem
{
    public IGroup Group { get; } = new Group(typeof(MoveableComponent), typeof(ShootingComponent), typeof(PlayerComponent));
        
    public void Process(IEntityComponentAccessor entityComponentAccessor, Entity entity, ElapsedTime elapsedTime)
    {
        var playerComponent = entityComponentAccessor.GetComponent<PlayerComponent>(entity);
        
        var gamepadState = GamePad.GetState(playerComponent.PlayerIndex);
        var keyboardState = Keyboard.GetState();
        var forwardChange = 0f;
        var strafeChange = 0f;
        var rotationChange = 0f;
        var isFiring = false;

        if (gamepadState.DPad.Up == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.W))
        { forwardChange = 1; }
        else if (gamepadState.DPad.Down == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.S))
        { forwardChange = -1;}
            
        if (gamepadState.DPad.Left == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.A))
        { strafeChange = -1; }
        else if (gamepadState.DPad.Right == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.D))
        { strafeChange = 1; }
            
        if (gamepadState.Triggers.Left != 0 || keyboardState.IsKeyDown(Keys.Q))
        { rotationChange = -1; }
        else if (gamepadState.Triggers.Right != 0 ||keyboardState.IsKeyDown(Keys.E))
        { rotationChange = 1; }
        
        if (gamepadState.Buttons.X == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.Space))
        { isFiring = true; }
            
        var moveableComponent = entityComponentAccessor.GetComponent<MoveableComponent>(entity);
        moveableComponent.MovementChange = new Vector2(strafeChange, forwardChange);
        moveableComponent.DirectionChange = rotationChange;

        var shootingComponent = entityComponentAccessor.GetComponent<ShootingComponent>(entity);
        shootingComponent.IsFiring = isFiring;
    }
}