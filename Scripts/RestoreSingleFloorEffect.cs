public partial class RestoreSingleFloorEffect : KonpeitoEffect
{
    public override void Execute()
    {
        var floorManager = GetNode<FloorManager>("FloorManager");
        floorManager.RestoreRandomFloor();
    }
}
