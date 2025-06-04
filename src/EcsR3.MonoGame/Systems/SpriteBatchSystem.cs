using EcsR3.Entities;
using EcsR3.Entities.Accessors;
using EcsR3.Groups;
using EcsR3.MonoGame.Wrappers;
using EcsR3.Systems;

namespace EcsR3.MonoGame.Systems;

public abstract class SpriteBatchSystem : IGroupSystem
{
    public abstract IGroup Group { get; }
        
    protected IEcsRxSpriteBatch EcsRxSpriteBatch { get; }

    protected SpriteBatchSystem(IEcsRxSpriteBatch ecsRxSpriteBatch)
    {
        EcsRxSpriteBatch = ecsRxSpriteBatch;
    }

    public virtual void PreDraw() { EcsRxSpriteBatch.Begin(); }
    public abstract void Process(IEntityComponentAccessor entityComponentAccessor, Entity entity);
    public virtual void PostDraw() { EcsRxSpriteBatch.End(); }
}