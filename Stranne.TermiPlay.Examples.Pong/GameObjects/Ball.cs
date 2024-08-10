using Stranne.TermiPlay.Examples.LeapAndDoge.GameObjects;
using Stranne.TermiPlay.GameEngine;
using System.Drawing;
using System.Numerics;

internal sealed class Ball(Vector2 boardSize) : PositionedGameObject(boardSize / 2, 1, 2)
{
    private const double initialSpeed = 0.2;
    private const double speedIncrease = 0.025;

    private readonly TimeSpan speedIncreaseInterval = TimeSpan.FromSeconds(5);

    private double speed = initialSpeed;

    public Vector2 Direction { get; set; } = InitialDirection();

    public override void OnUpdate(UpdateData data)
    {
        ApplyAnyBorderBounce();
        position = new(
            Math.Clamp(position.X + Direction.X * (float)speed, 0, data.BoardSize.X),
            Math.Clamp(position.Y + Direction.Y * (float)speed, 0, data.BoardSize.Y));
    }

    public override void OnRender(IGameObjectRenderer renderer) =>
        renderer.UpdateCells(
            TopLeft,
            BottomRight,
            '█',
            Color.Red);

    public void IncreaseSpeed() =>
        speed += speedIncrease;

    private void ApplyAnyBorderBounce()
    {
        if (Math.Round(TopLeft.Y) <= 0)
        {
            position.Y = Math.Abs(position.Y) + 1;
            Direction = new Vector2(Direction.X, -Direction.Y);
        }
        else if (Math.Round(BottomRight.Y) >= boardSize.Y)
        {
            position.Y = boardSize.Y - (boardSize.Y - position.Y) - 1;
            Direction = new Vector2(Direction.X, -Direction.Y);
        }
    }

    private static Vector2 InitialDirection()
    {
        var angle = new Random().NextDouble() * Math.PI / 2 - Math.PI / 4;

        if (new Random().Next(2) == 0)
            angle += Math.PI;

        var x = (float)Math.Cos(angle);
        var y = (float)Math.Sin(angle);

        return new Vector2(x, y);
    }
}
