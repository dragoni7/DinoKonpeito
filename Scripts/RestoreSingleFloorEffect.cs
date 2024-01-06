public partial class RestoreSingleFloorEffect : KonpeitoEffect
{
    public override void Execute()
    {
        var floorManager = GetNode<FloorManager>("/root/Game/FloorManager");
        floorManager.CallDeferred(FloorManager.MethodName.RestoreRandomFloor);
        //floorManager.RestoreRandomFloor();
        QueueFree();
    }
}
