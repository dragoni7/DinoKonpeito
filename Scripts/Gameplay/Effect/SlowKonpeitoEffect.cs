using Godot;

public partial class SlowKonpeitoEffect : KonpeitoEffect
{
    [Export]
    private Timer _timer;

    public override void Execute()
    {
        KonpeitoManager.GetInstance(this).SpeedModifier = 0.5f;
        _timer.Start();
    }

    private void OnDurationTimerTimeout()
    {
        KonpeitoManager.GetInstance(this).SpeedModifier = 1f;
        QueueFree();
    }
}
