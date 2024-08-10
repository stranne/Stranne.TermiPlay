using System.Numerics;

namespace Stranne.TermiPlay.GameEngine;

public sealed class UpdateData
{
    public required TimeSpan DeltaTime { get; init; }
    public required ConsoleKeyInfo? KeyPressed { get; init; }
    public required Vector2 BoardSize { get; init; }
}
