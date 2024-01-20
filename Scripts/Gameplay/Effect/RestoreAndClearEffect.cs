using Godot;

[Tool]
public partial class RestoreAndClearEffect : KonpeitoEffect
{
    public override void Execute()
    {
        KonpeitoSpawner spawner = GetNode<KonpeitoSpawner>("/root/Game/KonpeitoSpawner");
        spawner.CanSpawn = false;

        KonpeitoManager.GetInstance(this).HitAllKonpeito();
        FloorManager.GetInstance(this).CallDeferred(FloorManager.MethodName.RestoreAllFloor);

        var tween = GetTree().CreateTween();
        tween.Finished += OnFinished;
        tween.TweenProperty(spawner, "CanSpawn", true, 2.5);
    }

    private void OnFinished()
    {
        QueueFree();
    }
}
