using System.Drawing;
using System.Numerics;

namespace Stranne.TermiPlay.GameEngine;

internal sealed class Cell
{
    public required Vector2 Position { get; init; }
    public required GameObject? Owner { get; init; }
    public required bool ToDraw { get; set; }

    public char Content { get; set; }
    public Color? Color { get; set; }
}
