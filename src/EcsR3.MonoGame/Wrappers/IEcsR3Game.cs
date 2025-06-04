using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using R3;

namespace EcsR3.MonoGame.Wrappers;

public interface IEcsR3Game : IGameScheduler
{
    Observable<Unit> GameLoading { get; }
    Observable<Unit> GameUnloading { get; }
    IEcsR3GraphicsDeviceManager EcsR3GraphicsDeviceManager { get; }
    IEcsR3SpriteBatch EcsR3SpriteBatch { get; }
    IEcsR3GraphicsDevice EcsR3GraphicsDevice { get; }
    LaunchParameters LaunchParameters { get; }
    GameComponentCollection Components { get; }
    TimeSpan InactiveSleepTime { get; }
    TimeSpan MaxElapsedTime { get; }
    bool IsActive { get; }
    bool IsMouseVisible { get; set; }
    TimeSpan TargetElapsedTime { get; }
    bool IsFixedTimeStep { get; set; }
    GameServiceContainer Services { get; }
    IEcsR3ContentManager EcsR3ContentManager { get; }
    GraphicsDevice GraphicsDevice { get; }
    GameWindow Window { get; }
    void Dispose();
    void Exit();
    void ResetElapsedTime();
    void SuppressDraw();
    void RunOneFrame();
    void Run();
    void Run(GameRunBehavior runBehavior);
    void Tick();
    event EventHandler<EventArgs> Activated;
    event EventHandler<EventArgs> Deactivated;
    event EventHandler<EventArgs> Disposed;
    event EventHandler<EventArgs> Exiting;
}