using Godot;

public partial class SlowKonpeitoEffect : KonpeitoEffect
{
    [Export]
    private Timer _timer;

    public override void Execute()
    {
        GetTree().CallGroup("Konpeito", Konpeito.MethodName.ModifySpeed, 0.5f);
        GetNode<KonpeitoManager>("/root/Game/KonpeitoManager").SpeedModifier = 0.5f;

        _timer.Start();
    }

    private void OnDurationTimerTimeout()
    {
        Undo();
    }

    private void Undo()
    {
        GetTree().CallGroup("Konpeito", Konpeito.MethodName.ResetSpeed);
        GetNode<KonpeitoManager>("/root/Game/KonpeitoManager").SpeedModifier = 1f;
        QueueFree();
    }
}
