using Microsoft.Xna.Framework;

namespace EcsR3.MonoGame.Wrappers;

public class EcsRxGraphicsDeviceManager : GraphicsDeviceManager, IEcsRxGraphicsDeviceManager
{
    public EcsRxGraphicsDeviceManager(Game game) : base(game)
    {}
}