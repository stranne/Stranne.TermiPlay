using Stranne.TermiPlay.GameEngine;
using System.Numerics;

namespace Stranne.TermiPlay.Examples.LeapAndDoge.GameObjects;

public abstract class PositionedGameObject(Vector2 position, float height, float width) : GameObject
{
    protected Vector2 position = position;
    public readonly float height = height;
    public readonly float width = width;

    public Vector2 TopLeft => position;
    public Vector2 BottomRight => new(TopLeft.X + width, TopLeft.Y + height);
}
