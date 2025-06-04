using Comora;
using EcsR3.Entities;
using EcsR3.Entities.Accessors;
using EcsR3.Extensions;
using EcsR3.Groups;
using EcsR3.MonoGame.Components;
using EcsR3.MonoGame.Systems;
using EcsR3.MonoGame.Wrappers;
using EcsR3.Plugins.Transforms.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SystemsR3.Attributes;
using SystemsR3.Types;

namespace EcsR3.MonoGame.Examples.Asteroids.Game.Systems.Infrastructure;

[Priority(PriorityTypes.SuperLow)]
public class SpriteDrawingSystem : SpriteBatchSystem
{
    public override IGroup Group { get; } =  new Group(typeof(SpriteComponent), typeof(Transform2DComponent));
        
    private readonly ICamera _camera;

    public SpriteDrawingSystem(IEcsR3SpriteBatch ecsR3SpriteBatch, ICamera camera) : base(ecsR3SpriteBatch)
    {
        _camera = camera;
    }

    public override void PreDraw()
    { (EcsR3SpriteBatch as SpriteBatch).Begin(_camera); }

    public override void Process(IEntityComponentAccessor entityComponentAccessor, Entity entity)
    {
        var spriteComponent = entityComponentAccessor.GetComponent<SpriteComponent>(entity);
        var transformComponent = entityComponentAccessor.GetComponent<Transform2DComponent>(entity);
        var transform = transformComponent.Transform;
            
        var origin = new Vector2(spriteComponent.Sprite.Width / 2f, spriteComponent.Sprite.Height / 2f);
        EcsR3SpriteBatch.Draw(spriteComponent.Sprite, transform.Position, null, Color.White, transform.Rotation, origin, transform.Scale, SpriteEffects.None, 0);
    }
}