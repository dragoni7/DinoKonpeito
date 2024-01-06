using Godot;
using Godot.Collections;

public partial class Floor : Node2D
{
    [Signal]
    public delegate void DestroyedEventHandler(Vector2 position);
    public void OnHit(Array<StringName> groups)
    {
        QueueFree();
        EmitSignal(SignalName.Destroyed, Position);
    }
}
