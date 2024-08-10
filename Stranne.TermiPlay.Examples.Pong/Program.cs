using Stranne.TermiPlay.GameEngine;

using var gameEngine = GameEngine.Instance;

var ball = new Ball(gameEngine.BoardSize);
var humanPlayer = new HumanPlayer(gameEngine.BoardSize);
var computerPlayer = new ComputerPlayer(gameEngine.BoardSize, ball);
var playerCollission = new PlayerBallCollission(ball, humanPlayer, computerPlayer);
var winingConditionCheck = new WiningConditionCheck(ball);

gameEngine.GameObjects.AddRange([
    playerCollission,
    winingConditionCheck,
    ball,
    humanPlayer,
    computerPlayer,
]);

await gameEngine.Start();
