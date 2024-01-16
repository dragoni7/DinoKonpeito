using Godot;

public partial class RestoreAndClearEffect : KonpeitoEffect
{
    public override void Execute()
    {
        KonpeitoManager.GetInstance<KonpeitoManager>(this).HitAllKonpeito();
        var floorManager = FloorManager.GetInstance<FloorManager>(this);
        floorManager.CallDeferred(FloorManager.MethodName.RestoreAllFloor);
        QueueFree();
    }
}
