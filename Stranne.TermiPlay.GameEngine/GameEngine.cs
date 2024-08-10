using System.Numerics;
using System.Text;

namespace Stranne.TermiPlay.GameEngine;

public sealed class GameEngine : IDisposable
{
    public static GameEngine Instance => instance ??= new();
    private static GameEngine? instance;

    private readonly ConsoleRenderer consoleRenderer = new();
    private DateTime lastTimestamp;

    private readonly CancellationTokenSource cancellationToken = new();
    private readonly Task mainThread;


    public List<GameObject> GameObjects { get; } = [];
    public Vector2 BoardSize { get; set; }
    public TimeSpan TimeBeteenUpdates { get; set; } = TimeSpan.FromMilliseconds(1000 / 60);

    private GameEngine()
    {
        BoardSize = new(Console.WindowWidth, Console.WindowHeight);
        Console.CursorVisible = false;
        mainThread = new Task(Work);
    }

    public Task Start()
    {
        Console.CursorVisible = false;
        mainThread.Start();
        return mainThread;
    }

    public void End() =>
        cancellationToken.Cancel();

    public void Dispose()
    {
        cancellationToken.Cancel();
        mainThread.Wait();
        mainThread.Dispose();
        Console.CursorVisible = true;
    }

    private void Work()
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var timestamp = DateTime.Now;
            var deltaTime = timestamp.Subtract(lastTimestamp);
            var keyPressed = GetKeyPressed();

            foreach (var gameObject in GameObjects)
            {
                var timeBeteenUpdate = gameObject.TimeBeteenUpdates;
                if (gameObject.lastUpdate == null ||
                    timeBeteenUpdate == null ||
                    gameObject.lastUpdate + timeBeteenUpdate <= timestamp)
                {
                    var updateData = new UpdateData
                    {
                        DeltaTime = deltaTime,
                        KeyPressed = keyPressed,
                        BoardSize = BoardSize
                    };
                    gameObject.OnUpdate(updateData);
                    gameObject.lastUpdate = timestamp;
                }

                if (cancellationToken.IsCancellationRequested)
                    return;
            }

            foreach (var gameObject in GameObjects)
            {
                var gameObjectRenderer = new GameObjectRenderer(gameObject, BoardSize);
                gameObject.OnRender(gameObjectRenderer);
                consoleRenderer.UpdateGameObjectCells(gameObjectRenderer, BoardSize);

                if (cancellationToken.IsCancellationRequested)
                    return;
            }

            consoleRenderer.Render();

            WaitForNextFrame(timestamp);
        }
    }

    private static ConsoleKeyInfo? GetKeyPressed() =>
        Console.KeyAvailable
            ? Console.ReadKey(true)
        : null;

    private void WaitForNextFrame(DateTime timestamp)
    {
        var delay = (int)Math.Max(0, (TimeBeteenUpdates - (DateTime.Now - timestamp)).TotalMilliseconds);
        lastTimestamp = DateTime.Now;
        Thread.Sleep(delay);
    }
}
