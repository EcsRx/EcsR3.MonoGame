using Microsoft.Xna.Framework;

namespace EcsR3.MonoGame.Wrappers;

public class EcsR3GraphicsDeviceManager : GraphicsDeviceManager, IEcsR3GraphicsDeviceManager
{
    public EcsR3GraphicsDeviceManager(Game game) : base(game)
    {}
}