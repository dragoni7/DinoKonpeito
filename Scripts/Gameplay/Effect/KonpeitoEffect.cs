using Godot;

[Tool]
public partial class KonpeitoEffect : Node
{
    public virtual void Execute()
    {
        QueueFree();
    }
}
