using System;
using System.Collections.Generic;
using EcsR3.Components.Database;
using EcsR3.Computeds.Components;
using EcsR3.Computeds.Components.Conventions;
using EcsR3.Entities;
using EcsR3.MonoGame.Examples.Asteroids.Extensions;
using EcsR3.MonoGame.Examples.Asteroids.Game.Components;
using EcsR3.Plugins.Transforms.Components;
using Microsoft.Xna.Framework;
using R3;

namespace EcsR3.MonoGame.Examples.Asteroids.Game.Computed;

public class ComputedRuntimeColliders : ComputedFromComponentGroup<Dictionary<int, Rectangle>, ColliderComponent, Transform2DComponent>
{
    public ComputedRuntimeColliders(IComponentDatabase componentDatabase, IComputedComponentGroup<ColliderComponent, Transform2DComponent> dataSource) : base(componentDatabase, dataSource)
    {
        ComputedData = new Dictionary<int, Rectangle>();
    }

    protected override Observable<Unit> RefreshWhen()
    { return Observable.Never<Unit>(); }

    protected override void UpdateComputedData(ReadOnlyMemory<(Entity, ColliderComponent, Transform2DComponent)> componentData)
    {
        var componentDataSpan = componentData.Span;
        for (var i = 0; i < componentDataSpan.Length; i++)
        {
            var (entity, colliderComponent, transformComponent) = componentDataSpan[i];
            var collider = transformComponent.Transform.GetCollisionArea(colliderComponent);
            ComputedData[entity.Id] = collider;
        }
    }
}