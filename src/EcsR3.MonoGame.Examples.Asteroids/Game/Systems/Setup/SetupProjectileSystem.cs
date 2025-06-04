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

namespace EcsR3.MonoGame.Examples.Asteroids.Game.Systems.Setup;

public class SetupProjectileSystem : ISetupSystem
{
    public IGroup Group { get; } = new Group(typeof(ProjectileComponent), typeof(SpriteComponent), typeof(Transform2DComponent), typeof(ColliderComponent), typeof(MoveableComponent));
        
    public IEcsR3ContentManager ContentManager { get; }
        
    public SetupProjectileSystem(IEcsR3ContentManager contentManager)
    {
        ContentManager = contentManager;
    }

    public void Setup(IEntityComponentAccessor entityComponentAccessor, Entity entity)
    {
        var spriteComponent = entityComponentAccessor.GetComponent<SpriteComponent>(entity);
        spriteComponent.Sprite = ContentManager.Load<Texture2D>("laser");

        var colliderComponent = entityComponentAccessor.GetComponent<ColliderComponent>(entity);
        colliderComponent.Width = spriteComponent.Sprite.Width;
        colliderComponent.Height = spriteComponent.Sprite.Height;
    }
}