using System.Drawing;
using System.Numerics;

namespace Stranne.TermiPlay.GameEngine;

internal sealed class ConsoleRenderer
{
    private readonly IList<Cell> cells = [];

    public void UpdateGameObjectCells(GameObjectRenderer gameObjectRenderer, Vector2 boardSize)
    {
        var gameObjectsPreviousCells = cells.Where(cell => cell.Owner == gameObjectRenderer.gameObject).ToList();
        DrawNewCells(gameObjectRenderer, boardSize);
        UpdateOldCells(gameObjectRenderer, gameObjectsPreviousCells);
    }

    private void UpdateOldCells(GameObjectRenderer gameObjectRenderer, List<Cell> gameObjectsPreviousCells)
    {
        var cellsToRestore = gameObjectsPreviousCells
                    .Where(cell => !gameObjectRenderer.cells
                        .Any(cell2 => cell.Position == cell2.Position))
                    .ToList();
        foreach (var cell in cellsToRestore)
        {
            cells.Remove(cell);
            var replacableCell = cells
                .Where(c => c.Position == cell.Position && c.Owner?.id != gameObjectRenderer.gameObject.id)
                .OrderByDescending(cell => cell.Owner?.RenderPriority)
                .FirstOrDefault();

            if (replacableCell != null)
                replacableCell.ToDraw = true;
            else
                cells.Add(new Cell
                {
                    Position = cell.Position,
                    Owner = null,
                    ToDraw = true,
                    Content = ' ',
                    Color = null
                });
        }
    }

    private void DrawNewCells(GameObjectRenderer gameObjectRenderer, Vector2 boardSize)
    {
        foreach (var cell in gameObjectRenderer.cells)
        {
            var existingCell = cells.SingleOrDefault(c => c.Position == cell.Position && c.Owner?.id == cell.Owner?.id);
            if (existingCell != null)
            {
                existingCell.ToDraw = cell.ToDraw;
                existingCell.Content = cell.Content;
                existingCell.Color = cell.Color;
            }
            else
                cells.Add(cell);
        }
    }

    internal void Render()
    {
        var cells = GetCellsToRender();
        foreach (var cell in cells)
        {
            Console.SetCursorPosition((int)cell.Position.X, (int)cell.Position.Y);
            if (cell.Color.HasValue)
                SetColor(cell.Color.Value);
            Console.Write(cell.Content);
            if (cell.Color.HasValue)
                ResetColor();
            cell.ToDraw = false;
        }
    }

    private List<Cell> GetCellsToRender() =>
        cells.GroupBy(cell => cell.Position)
            .Select(group => group.OrderByDescending(cell => cell.Owner?.RenderPriority).First())
            .Where(cell => cell.ToDraw)
            .ToList();

    private static void SetColor(Color color) =>
        Console.Write($"\x1b[38;2;{color.R};{color.G};{color.B}m");

    private static void ResetColor() =>
        Console.Write("\x1b[0m");
}
