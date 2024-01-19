using Godot;

[Tool]
public partial class RestoreAndClearEffect : KonpeitoEffect
{
    public override void Execute()
    {
        KonpeitoManager.GetInstance(this).HitAllKonpeito();
        FloorManager.GetInstance(this).CallDeferred(FloorManager.MethodName.RestoreAllFloor);
        QueueFree();
    }
}
