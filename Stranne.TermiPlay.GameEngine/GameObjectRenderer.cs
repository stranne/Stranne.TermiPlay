using System.Drawing;
using System.Numerics;

namespace Stranne.TermiPlay.GameEngine;

internal sealed class GameObjectRenderer(GameObject gameObject, Vector2 boardSize) : IGameObjectRenderer
{
    public readonly IList<Cell> cells = [];
    public readonly GameObject gameObject = gameObject;
    public readonly Vector2 boardSize = boardSize;

    public void UpdateCell(Vector2 position, char? content, Color? color)
    {
        if (position.X < 0 || position.Y < 0 || position.X > boardSize.X || position.Y > boardSize.Y)
            throw new IndexOutOfRangeException($"{gameObject.GetType().Name}'s cell {position} is outside the bounds of the board size {boardSize}.");

        cells.Add(new Cell
        {
            Position = position,
            Owner = gameObject,
            Content = content ?? ' ',
            Color = color,
            ToDraw = true
        });
    }

    public void UpdateCells(Vector2 positionTopLeft, Vector2 positionBottomRight, char? content, Color? color)
    {
        var startX = (int)positionTopLeft.X;
        var startY = (int)positionTopLeft.Y;
        var endX = (int)positionBottomRight.X;
        var endY = (int)positionBottomRight.Y;

        for (int y = startY; y <= endY; y++)
            for (int x = startX; x <= endX; x++)
                UpdateCell(new Vector2(x, y), content, color);

    }
}
