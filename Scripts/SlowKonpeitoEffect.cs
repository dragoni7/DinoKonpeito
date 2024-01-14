using Godot;

public partial class SlowKonpeitoEffect : KonpeitoEffect
{
    [Export]
    private Timer _timer;

    public override void Execute()
    {
        GetNode<KonpeitoManager>("/root/Game/KonpeitoManager").SpeedModifier = 0.5f;

        _timer.Start();
    }

    private void OnDurationTimerTimeout()
    {
        Undo();
    }

    private void Undo()
    {
        GetNode<KonpeitoManager>("/root/Game/KonpeitoManager").SpeedModifier = 1f;
        QueueFree();
    }
}
