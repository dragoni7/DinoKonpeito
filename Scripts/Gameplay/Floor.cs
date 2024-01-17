using Godot;
using Godot.Collections;

public partial class Floor : Node2D, ITakesHits
{
    [Signal]
    public delegate void DestroyedEventHandler(Vector2 position);

    public void OnHit(Array<StringName> groups)
    {
        EmitSignal(SignalName.Destroyed, Position);
        QueueFree();
    }
}
