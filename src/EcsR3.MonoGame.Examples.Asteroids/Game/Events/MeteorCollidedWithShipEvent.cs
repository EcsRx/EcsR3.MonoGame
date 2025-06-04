using EcsR3.Entities;

namespace EcsR3.MonoGame.Examples.Asteroids.Game.Events;

public readonly struct MeteorCollidedWithShipEvent
{
    public readonly Entity MeteorEntity;
    public readonly Entity ShipEntity;

    public MeteorCollidedWithShipEvent(Entity meteorEntity, Entity shipEntity)
    {
        MeteorEntity = meteorEntity;
        ShipEntity = shipEntity;
    }
}