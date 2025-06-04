using EcsR3.Entities;
using EcsR3.Entities.Accessors;
using EcsR3.Groups;
using EcsR3.MonoGame.Wrappers;
using EcsR3.Systems;

namespace EcsR3.MonoGame.Systems;

public abstract class SpriteBatchSystem : IGroupSystem
{
    public abstract IGroup Group { get; }
        
    protected IEcsR3SpriteBatch EcsR3SpriteBatch { get; }

    protected SpriteBatchSystem(IEcsR3SpriteBatch ecsR3SpriteBatch)
    {
        EcsR3SpriteBatch = ecsR3SpriteBatch;
    }

    public virtual void PreDraw() { EcsR3SpriteBatch.Begin(); }
    public abstract void Process(IEntityComponentAccessor entityComponentAccessor, Entity entity);
    public virtual void PostDraw() { EcsR3SpriteBatch.End(); }
}