using Stranne.TermiPlay.GameEngine;

internal sealed class WiningConditionCheck(Ball ball) : GameObject
{
    private readonly Ball ball = ball;

    public override void OnUpdate(UpdateData data)
    {
        if (ball.TopLeft.X <= 0)
        {
            GameEngine.Instance.End();
            Console.WriteLine("Computer wins!");
        }
        else if (ball.BottomRight.X >= GameEngine.Instance.BoardSize.X)
        {
            GameEngine.Instance.End();
            Console.WriteLine("Human wins!");
        }
    }

    public override void OnRender(IGameObjectRenderer renderer)
    { }
}
