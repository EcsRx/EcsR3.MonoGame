using System;
using Microsoft.Xna.Framework.Content;

namespace EcsR3.MonoGame.Wrappers;

public class EcsR3ContentManager : IEcsR3ContentManager
{
    public EcsR3ContentManager(ContentManager internalManager)
    { InternalManager = internalManager; }


    public ContentManager InternalManager { get; }
    public void Dispose() =>InternalManager.Dispose();
    public T LoadLocalized<T>(string assetName) => InternalManager.LoadLocalized<T>(assetName);
    public T Load<T>(string assetName) => InternalManager.Load<T>(assetName);
    public void Unload() => InternalManager.Unload();

    public string RootDirectory
    {
        get => InternalManager.RootDirectory;
        set => InternalManager.RootDirectory = value;
    }

    public IServiceProvider ServiceProvider => InternalManager.ServiceProvider;
}