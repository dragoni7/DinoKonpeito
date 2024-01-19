using Godot;

[Tool]
public partial class RestoreSingleFloorEffect : KonpeitoEffect
{
    public override void Execute()
    {
        FloorManager.GetInstance(this).CallDeferred(FloorManager.MethodName.RestoreFloor);
        QueueFree();
    }
}
