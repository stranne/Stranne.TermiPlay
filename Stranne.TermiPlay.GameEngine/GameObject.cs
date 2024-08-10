namespace Stranne.TermiPlay.GameEngine;

public abstract class GameObject
{
    internal readonly Guid id = Guid.NewGuid();
    internal DateTime? lastUpdate;
    public virtual int RenderPriority { get; init; } = 100;

    public virtual TimeSpan? TimeBeteenUpdates => null;

    public abstract void OnUpdate(UpdateData data);

    public abstract void OnRender(IGameObjectRenderer renderer);
}
