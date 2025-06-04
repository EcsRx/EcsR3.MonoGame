using System;
using Microsoft.Xna.Framework.Content;

namespace EcsR3.MonoGame.Wrappers;

public interface IEcsR3ContentManager
{
    ContentManager InternalManager { get; }
        
    void Dispose();
    T LoadLocalized<T>(string assetName);
    T Load<T>(string assetName);
    void Unload();
    string RootDirectory { get; set; }
    IServiceProvider ServiceProvider { get; }
}