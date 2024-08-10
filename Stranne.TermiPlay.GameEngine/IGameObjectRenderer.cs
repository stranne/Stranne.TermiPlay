using System.Drawing;
using System.Numerics;

namespace Stranne.TermiPlay.GameEngine;

public interface IGameObjectRenderer
{
    void UpdateCell(Vector2 position, char? content, Color? color);
    void UpdateCells(Vector2 positionToLeft, Vector2 positionBottomRight, char? content, Color? color);
}
