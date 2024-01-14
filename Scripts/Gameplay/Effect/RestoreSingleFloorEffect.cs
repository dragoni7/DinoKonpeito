public partial class RestoreSingleFloorEffect : KonpeitoEffect
{
    public override void Execute()
    {
        var floorManager = FloorManager.GetInstance<FloorManager>(this);
        floorManager.CallDeferred(FloorManager.MethodName.RestoreFloor);
        QueueFree();
    }
}
