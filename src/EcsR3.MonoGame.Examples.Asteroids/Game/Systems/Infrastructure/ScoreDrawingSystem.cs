using EcsR3.Entities;
using EcsR3.Entities.Accessors;
using EcsR3.Extensions;
using EcsR3.Groups;
using EcsR3.MonoGame.Examples.Asteroids.Game.Components;
using EcsR3.MonoGame.Systems;
using EcsR3.MonoGame.Wrappers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SystemsR3.Attributes;
using SystemsR3.Types;

namespace EcsR3.MonoGame.Examples.Asteroids.Game.Systems.Infrastructure;

[Priority(PriorityTypes.SuperLow)]
public class ScoreDrawingSystem : SpriteBatchSystem
{
    public override IGroup Group { get; } =  new Group(typeof(PlayerComponent));
    public SpriteFont GameFont { get; }
        
    public ScoreDrawingSystem(IEcsRxSpriteBatch ecsRxSpriteBatch, IEcsRxContentManager contentManager) : base(ecsRxSpriteBatch)
    {
        GameFont = contentManager.Load<SpriteFont>("GameFont");
    }

    public override void Process(IEntityComponentAccessor entityComponentAccessor, Entity entity)
    {
        var playerComponent = entityComponentAccessor.GetComponent<PlayerComponent>(entity);

        var playerNumber = playerComponent.PlayerIndex + 1;
        var position = new Vector2(24, playerNumber * 24);
        EcsRxSpriteBatch.DrawString(GameFont, $"Player {playerNumber}: {playerComponent.Score.ToString()}", position, Color.White);
    }
}