using System;
using EcsR3.Entities;
using EcsR3.Entities.Accessors;
using EcsR3.Extensions;
using EcsR3.Groups;
using EcsR3.MonoGame.Components;
using EcsR3.MonoGame.Examples.Asteroids.Extensions;
using EcsR3.MonoGame.Examples.Asteroids.Game.Components;
using EcsR3.MonoGame.Examples.Asteroids.Types;
using EcsR3.MonoGame.Wrappers;
using EcsR3.Plugins.Transforms.Components;
using EcsR3.Systems.Reactive;
using Microsoft.Xna.Framework.Graphics;
using SystemsR3.Plugins.Transforms.Extensions;
using Vector2 = System.Numerics.Vector2;

namespace EcsR3.MonoGame.Examples.Asteroids.Game.Systems.Setup;

public class SetupMeteorSystem : ISetupSystem
{
    public IGroup Group { get; } = new Group(typeof(MeteorComponent), typeof(SpriteComponent), typeof(Transform2DComponent), typeof(ColliderComponent), typeof(MoveableComponent));
        
    public IEcsRxContentManager ContentManager { get; }
    public IEcsRxGraphicsDevice GraphicsDevice { get; }
    public Random Random { get; } = new Random();
        
    public SetupMeteorSystem(IEcsRxContentManager contentManager, IEcsRxGraphicsDevice graphicsDevice)
    {
        ContentManager = contentManager;
        GraphicsDevice = graphicsDevice;
    }

    public void Setup(IEntityComponentAccessor entityComponentAccessor, Entity entity)
    {
        var spriteComponent = entityComponentAccessor.GetComponent<SpriteComponent>(entity);
        spriteComponent.Sprite = ContentManager.Load<Texture2D>("meteor");

        var colliderComponent = entityComponentAccessor.GetComponent<ColliderComponent>(entity);
        colliderComponent.Width = spriteComponent.Sprite.Width;
        colliderComponent.Height = spriteComponent.Sprite.Height;
            
        var moveableComponent = entityComponentAccessor.GetComponent<MoveableComponent>(entity);
        moveableComponent.MovementChange = new Vector2(0, 1);

        SetupTransform(entityComponentAccessor, entity);
    }

    public void SetupTransform(IEntityComponentAccessor entityComponentAccessor, Entity entity)
    {
        var meteorComponent = entityComponentAccessor.GetComponent<MeteorComponent>(entity);
        var transformComponent = entityComponentAccessor.GetComponent<Transform2DComponent>(entity);
        var viewTransform = transformComponent.Transform;
        viewTransform.Scale = Vector2.One / (int)meteorComponent.Type;
        if (meteorComponent.Type != MeteorType.Big) { return; }

        var spawnPosition = GetRandomSpawnPosition();
        var targetPosition = GetTargetPosition();
        viewTransform.Position = spawnPosition;
        viewTransform.Rotation = viewTransform.MGLookAt(targetPosition);
    }
        
    public Vector2 GetRandomSpawnPosition()
    {
        var bufferAmount = 64;
        var spawnAreaWidth = GraphicsDevice.Viewport.Width/2 + bufferAmount;
        var spawnAreaHeight = GraphicsDevice.Viewport.Height/2 + bufferAmount;
            
        var isVerticalSpawn = Random.NextBool();

        if (isVerticalSpawn)
        {
            var randomY = Random.Next(-spawnAreaHeight, spawnAreaHeight);
            var leftOrRight = Random.NextBool() ? -spawnAreaWidth : spawnAreaWidth;
            return new Vector2(leftOrRight, randomY);
        }
            
        var randomX = Random.Next(-spawnAreaWidth, spawnAreaWidth);
        var topOrBottom = Random.NextBool() ? -spawnAreaHeight : spawnAreaHeight;
        return new Vector2(randomX, topOrBottom);
    }

    public Vector2 GetTargetPosition()
    {
        var range = 128;
        var varianceX = Random.Next(-range, range);
        var varianceY = Random.Next(-range, range);
        return new Vector2(varianceX, varianceY);
    }
}