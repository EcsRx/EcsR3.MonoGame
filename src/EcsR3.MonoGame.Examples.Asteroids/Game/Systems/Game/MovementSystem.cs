using EcsR3.Components.Database;
using EcsR3.Computeds.Components.Registries;
using EcsR3.Entities;
using EcsR3.Entities.Accessors;
using EcsR3.Groups;
using EcsR3.MonoGame.Examples.Asteroids.Extensions;
using EcsR3.MonoGame.Examples.Asteroids.Game.Components;
using EcsR3.Plugins.Transforms.Components;
using EcsR3.Systems;
using EcsR3.Systems.Augments;
using EcsR3.Systems.Batching.Convention;
using R3;
using SystemsR3.Attributes;
using SystemsR3.Plugins.Transforms.Extensions;
using SystemsR3.Scheduling;
using SystemsR3.Threading;
using SystemsR3.Types;

namespace EcsR3.MonoGame.Examples.Asteroids.Game.Systems.Game;

[Priority(PriorityTypes.Higher)]
public class MovementSystem : BatchedSystem<HandlingComponent, MoveableComponent, Transform2DComponent>, ISystemPreProcessor
{
    public ITimeTracker TimeTracker { get; }
    
    private ElapsedTime _elapsedTime;
    
    public MovementSystem(IComponentDatabase componentDatabase, IEntityComponentAccessor entityComponentAccessor, IComputedComponentGroupRegistry computedComponentGroupRegistry, IThreadHandler threadHandler, ITimeTracker timeTracker) : base(componentDatabase, entityComponentAccessor, computedComponentGroupRegistry, threadHandler)
    {
        TimeTracker = timeTracker;
    }

    protected override Observable<Unit> ReactWhen()
    { return Observable.EveryUpdate(); }

    protected override void Process(Entity entity, HandlingComponent handlingComponent, MoveableComponent moveableComponent,
        Transform2DComponent transformComponent)
    {
        var movementSpeed = handlingComponent.MovementSpeed * (float)_elapsedTime.DeltaTime.TotalSeconds;
        var rotationSpeed = handlingComponent.RotationSpeed * (float)_elapsedTime.DeltaTime.TotalSeconds;
            
        var transform = transformComponent.Transform;
        transform.Position += transform.MGForward() * moveableComponent.MovementChange.Y * movementSpeed;
        transform.Position += transform.MGRight() * moveableComponent.MovementChange.X * movementSpeed;
        transform.Rotation += moveableComponent.DirectionChange * rotationSpeed;
    }

    public void BeforeProcessing()
    { _elapsedTime = TimeTracker.ElapsedTime; }
}