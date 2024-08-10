using Stranne.TermiPlay.Examples.LeapAndDoge.GameObjects;
using Stranne.TermiPlay.GameEngine;
using System.Drawing;
using System.Numerics;

internal abstract class Player(Vector2 boardSize, float leftX) : PositionedGameObject(
        new(leftX, (boardSize.Y - boardSize.Y / 3) / 2),
        boardSize.Y / 3,
        1)
{
    protected readonly Vector2 boardSize = boardSize;

    public override void OnRender(IGameObjectRenderer renderer) =>
        renderer.UpdateCells(TopLeft, BottomRight, '█', Color.White);
}
