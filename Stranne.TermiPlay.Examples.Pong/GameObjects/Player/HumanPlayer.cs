using Stranne.TermiPlay.GameEngine;
using System.Numerics;

internal sealed class HumanPlayer(Vector2 boardSize) : Player(boardSize, 0)
{
    public override TimeSpan? TimeBeteenUpdates => TimeSpan.FromSeconds(1 / (boardSize.X));

    public override void OnUpdate(UpdateData data)
    {
        var key = data.KeyPressed?.Key;
        if (key == null)
            return;
        else if (key == ConsoleKey.W)
            position.Y--;
        else if (key == ConsoleKey.S)
            position.Y++;
        else
            return;

        position.Y = Math.Clamp(position.Y, 0, boardSize.Y - height - 1);
    }
}
