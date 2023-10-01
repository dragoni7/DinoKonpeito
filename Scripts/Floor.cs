using Godot;

public partial class Floor : Node2D
{
    [Signal]
    public delegate void DestroyedEventHandler(Vector2 position);
    public void OnHit()
    {
        QueueFree();
        EmitSignal(SignalName.Destroyed, Position);
    }
}
