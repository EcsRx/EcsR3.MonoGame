using EcsR3.Entities;
using EcsR3.Entities.Accessors;
using EcsR3.Extensions;
using EcsR3.Groups;
using EcsR3.MonoGame.Components;
using EcsR3.MonoGame.Examples.Asteroids.Game.Components;
using EcsR3.MonoGame.Wrappers;
using EcsR3.Plugins.Transforms.Components;
using EcsR3.Systems.Reactive;
using Microsoft.Xna.Framework.Graphics;
using SystemsR3.Plugins.Transforms.Extensions;
using Vector2 = System.Numerics.Vector2;

namespace EcsR3.MonoGame.Examples.Asteroids.Game.Systems.Setup;

public class SetupShipSystem : ISetupSystem
{
    public IGroup Group { get; } = new Group(typeof(PlayerComponent), typeof(SpriteComponent), typeof(Transform2DComponent));
        
    public IEcsR3ContentManager ContentManager { get; }
    public IEcsR3GraphicsDevice GraphicsDevice { get; }

    public SetupShipSystem(IEcsR3ContentManager contentManager, IEcsR3GraphicsDevice graphicsDevice)
    {
        ContentManager = contentManager;
        GraphicsDevice = graphicsDevice;
    }

    public void Setup(IEntityComponentAccessor entityComponentAccessor, Entity entity)
    {
        var spriteComponent = entityComponentAccessor.GetComponent<SpriteComponent>(entity);
        spriteComponent.Sprite = ContentManager.Load<Texture2D>("ship");
            
        var colliderComponent = entityComponentAccessor.GetComponent<ColliderComponent>(entity);
        colliderComponent.Width = spriteComponent.Sprite.Width;
        colliderComponent.Height = spriteComponent.Sprite.Height;

        var screenCenterX = GraphicsDevice.Viewport.Width/2;
        var screenCenterY = GraphicsDevice.Viewport.Height/2;
        var viewComponent = entityComponentAccessor.GetComponent<Transform2DComponent>(entity);
        viewComponent.Transform.Position = new Vector2(0, 0);
    }
}