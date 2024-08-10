using Stranne.TermiPlay.GameEngine;
using System.Numerics;

internal sealed class PlayerBallCollission(Ball ball, Player leftPlayer, Player rightPlayer) : GameObject
{
    private readonly Ball ball = ball;
    private readonly Player leftPlayer = leftPlayer;
    private readonly Player rightPlayer = rightPlayer;

    public override void OnUpdate(UpdateData data)
    {
        Player? player = null;
        if (DoesCollide(leftPlayer, true))
            player = leftPlayer;
        else if (DoesCollide(rightPlayer, false))
            player = rightPlayer;

        if (player == null)
            return;

        ball.Direction = new Vector2(-ball.Direction.X, GetCollissionAngle(player));
        ball.IncreaseSpeed();
    }

    public override void OnRender(IGameObjectRenderer renderer)
    { }

    private bool DoesCollide(Player player, bool isLeftPlayer) =>
        (
            isLeftPlayer && ball.TopLeft.X <= player.BottomRight.X && ball.Direction.X < 0 ||
            !isLeftPlayer && ball.BottomRight.X >= player.TopLeft.X && ball.Direction.X > 0
        ) &&
            ball.BottomRight.Y >= player.TopLeft.Y &&
            ball.TopLeft.Y <= player.BottomRight.Y;

    private float GetCollissionAngle(Player player)
    {
        var ballCenterY = (ball.TopLeft.Y + ball.BottomRight.Y) / 2;
        var playerCenterY = (player.TopLeft.Y + player.BottomRight.Y) / 2;
        var relativeIntersectY = ballCenterY - playerCenterY;
        var normalizedRelativeIntersectionY = relativeIntersectY / ((player.BottomRight.Y - player.TopLeft.Y) / 2);
        var bounceAngle = normalizedRelativeIntersectionY * (float)(Math.PI / 4);

        return bounceAngle;
    }
}
