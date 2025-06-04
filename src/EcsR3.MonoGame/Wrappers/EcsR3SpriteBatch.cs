using Microsoft.Xna.Framework.Graphics;

namespace EcsR3.MonoGame.Wrappers;

public class EcsR3SpriteBatch : SpriteBatch, IEcsR3SpriteBatch
{
    public EcsR3SpriteBatch(GraphicsDevice graphicsDevice) : base(graphicsDevice)
    {}
}