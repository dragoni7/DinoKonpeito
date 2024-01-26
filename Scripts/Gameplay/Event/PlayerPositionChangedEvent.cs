using Godot;

public class PlayerPositionChangedEvent : IEvent
{
    public Vector2 NewPosition { get; private set; }

    public Vector2 OldPosition { get; private set; }
    public PlayerPositionChangedEvent(Vector2 newPos, Vector2 oldPos)
    {
        NewPosition = newPos;
        OldPosition = oldPos;
    }
}
