using Stranne.TermiPlay.GameEngine;
using System.Numerics;

internal sealed class ComputerPlayer(Vector2 boardSize, Ball ball) : Player(boardSize, boardSize.X - 3)
{
    private readonly float maxMovementSpeed = boardSize.Y / 3;

    private readonly Ball ball = ball;

    public override TimeSpan? TimeBeteenUpdates => TimeSpan.FromSeconds(width / 4);

    public override void OnUpdate(UpdateData data)
    {
        var ballCenterY = (ball.TopLeft.Y + ball.BottomRight.Y) / 2;
        var playerCenterY = (TopLeft.Y + BottomRight.Y) / 2;
        var relativeIntersectY = playerCenterY - ballCenterY;

        position.Y = Math.Clamp(
            position.Y - Math.Clamp(relativeIntersectY, -maxMovementSpeed, maxMovementSpeed),
            0,
            boardSize.Y - height - 1);
    }
}
