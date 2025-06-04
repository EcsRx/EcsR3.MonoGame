using System;
using Microsoft.Xna.Framework;
using R3;
using SystemsR3.Scheduling;

namespace EcsR3.MonoGame.Wrappers;

public class EcsR3Game : Game, IEcsR3Game
{
    private readonly Subject<ElapsedTime> _onPreUpdate, _onUpdate, _onPostUpdate, _onRender, _onPreRender, _onPostRender;
    private readonly Subject<Unit> _gameLoading;

    public Observable<ElapsedTime> OnUpdate => _onUpdate;
    public Observable<ElapsedTime> OnPreUpdate => _onPreUpdate;
    public Observable<ElapsedTime> OnPostUpdate => _onPostUpdate;
    public Observable<ElapsedTime> OnRender => _onRender;
    public Observable<ElapsedTime> OnPreRender => _onPreRender;
    public Observable<ElapsedTime> OnPostRender => _onPostRender;
    public Observable<Unit> GameLoading => _gameLoading;
    public Observable<Unit> GameUnloading { get; }
        
    public IEcsR3GraphicsDeviceManager EcsR3GraphicsDeviceManager { get; }
    public IEcsR3GraphicsDevice EcsR3GraphicsDevice { get; private set; }
    public IEcsR3SpriteBatch EcsR3SpriteBatch { get; private set; }
    public IEcsR3ContentManager EcsR3ContentManager { get; }
    public GameTime NativeGameTime { get; private set; }
    public ElapsedTime ElapsedTime { get; private set; }
        
    public EcsR3Game()
    {
        _onPreUpdate = new Subject<ElapsedTime>();
        _onUpdate = new Subject<ElapsedTime>();
        _onPostUpdate = new Subject<ElapsedTime>();
        _onRender = new Subject<ElapsedTime>();
        _onPreRender = new Subject<ElapsedTime>();
        _onPostRender = new Subject<ElapsedTime>();
        _gameLoading = new Subject<Unit>();
            
        EcsR3GraphicsDeviceManager = new EcsR3GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        EcsR3ContentManager = new EcsR3ContentManager(Content);
        IsMouseVisible = true;
        
        var observableSystemComponent = new ObservableSystemComponent(this);
        Components.Add(observableSystemComponent);

        GameUnloading = Observable.FromEventHandler<EventArgs>(
                x => Exiting += x, 
                x => Exiting -= x)
            .Select(x => Unit.Default);
    }

    protected override void LoadContent()
    {
        EcsR3GraphicsDevice = new EcsR3GraphicsDevice(GraphicsDevice);
        EcsR3SpriteBatch = new EcsR3SpriteBatch(GraphicsDevice);
        base.LoadContent();
        _gameLoading.OnNext(Unit.Default);
    }

    protected override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
        NativeGameTime = gameTime;
        ElapsedTime = new ElapsedTime(gameTime.ElapsedGameTime, gameTime.TotalGameTime);
            
        _onPreRender.OnNext(ElapsedTime);
        _onRender.OnNext(ElapsedTime);
        _onPostRender.OnNext(ElapsedTime);
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        NativeGameTime = gameTime;
        ElapsedTime = new ElapsedTime(gameTime.ElapsedGameTime, gameTime.TotalGameTime);
            
        _onPreUpdate.OnNext(ElapsedTime);
        _onUpdate.OnNext(ElapsedTime);
        _onPostUpdate.OnNext(ElapsedTime);
    }
        
    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        _onPreUpdate.Dispose();
        _onUpdate.Dispose();
        _onPostUpdate.Dispose();
        _onRender.Dispose();
        _onPreRender.Dispose();
        _onPostRender.Dispose();
        _gameLoading.Dispose();
    }
}