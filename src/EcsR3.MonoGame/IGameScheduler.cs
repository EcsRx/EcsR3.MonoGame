using R3;
using SystemsR3.Scheduling;

namespace EcsR3.MonoGame;

public interface IGameScheduler : IUpdateScheduler
{
    Observable<ElapsedTime> OnPreRender { get; }
    Observable<ElapsedTime> OnRender { get; }
    Observable<ElapsedTime> OnPostRender { get; }
}