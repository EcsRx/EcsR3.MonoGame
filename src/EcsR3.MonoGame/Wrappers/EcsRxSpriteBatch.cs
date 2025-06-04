using Microsoft.Xna.Framework.Graphics;

namespace EcsR3.MonoGame.Wrappers;

public class EcsRxSpriteBatch : SpriteBatch, IEcsRxSpriteBatch
{
    public EcsRxSpriteBatch(GraphicsDevice graphicsDevice) : base(graphicsDevice)
    {}
}