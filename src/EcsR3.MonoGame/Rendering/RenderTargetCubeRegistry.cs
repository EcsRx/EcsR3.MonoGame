using EcsR3.MonoGame.Wrappers;
using Microsoft.Xna.Framework.Graphics;

namespace EcsR3.MonoGame.Rendering;

public class RenderTargetCubeRegistry : RenderTargetRegistry<RenderTargetCube>, IRenderTargetCubeRegistry
{
    public RenderTargetCubeRegistry(IEcsR3GraphicsDevice graphicsDevice) : base(graphicsDevice)
    {
    }
        
    public RenderTargetCube CreateRenderTarget(int id, int size, bool mipmap, SurfaceFormat surfaceFormat, DepthFormat depthFormat)
    {
        var renderTarget = new RenderTargetCube(GraphicsDevice.InternalDevice, size, mipmap, surfaceFormat, depthFormat);
        _renderTargets.Add(id, renderTarget);
        return renderTarget;
    }
}