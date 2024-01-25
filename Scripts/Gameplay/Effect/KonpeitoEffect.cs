using Godot;

[Tool]
public partial class KonpeitoEffect : Node2D
{
    public virtual void Execute()
    {
        QueueFree();
    }
}
