using EcsR3.Entities;

namespace EcsR3.MonoGame.Examples.Asteroids.Game.Events;

public readonly struct MeteorCollidedWithProjectileEvent
{
    public readonly Entity MeteorEntity;
    public readonly Entity ProjectileEntity;

    public MeteorCollidedWithProjectileEvent(Entity meteorEntity, Entity projectileEntity)
    {
        MeteorEntity = meteorEntity;
        ProjectileEntity = projectileEntity;
    }
}