using System;
using System.Collections.Generic;
using EcsR3.MonoGame.Wrappers;

namespace EcsR3.MonoGame.Rendering;

public abstract class RenderTargetRegistry<T> : IRenderTargetRegistry<T>
    where T : class, IDisposable 
{
    protected readonly Dictionary<int, T> _renderTargets = new Dictionary<int, T>();
        
    public IEcsR3GraphicsDevice GraphicsDevice { get; }

    protected RenderTargetRegistry(IEcsR3GraphicsDevice graphicsDevice)
    {
        GraphicsDevice = graphicsDevice;
    }

    public IEnumerable<T> GetAllRenderTargets() => _renderTargets.Values;
    public T GetRenderTarget(int id) => _renderTargets[id];
       
    public void RegisterRenderTarget(int id, T renderTarget)
    { _renderTargets.Add(id, renderTarget); }

    public void RemoveRenderTarget(int id)
    {
        var renderTarget = _renderTargets[id];
        renderTarget.Dispose();
        _renderTargets.Remove(id);
    }
}