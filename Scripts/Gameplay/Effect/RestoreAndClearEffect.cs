using Godot;
using System;
using System.Linq;

[Tool]
public partial class RestoreAndClearEffect : KonpeitoEffect
{
    public override void Execute()
    {
        KonpeitoSpawner spawner = GetNode<KonpeitoSpawner>("/root/Game/KonpeitoSpawner");
        spawner.CanSpawn = false;

        HitAllKonpeito();
        FloorManager.GetInstance(this).CallDeferred(FloorManager.MethodName.RestoreAllFloor);

        var tween = GetTree().CreateTween();
        tween.Finished += OnFinished;
        tween.TweenProperty(spawner, "CanSpawn", true, 2.5);
    }

    private void HitAllKonpeito()
    {
        var konpeitos = KonpeitoManager.GetInstance(this).GetTree().GetNodesInGroup("Konpeito")
            .Where(n => n is Konpeito)
            .Select(n => n).Cast<Konpeito>()
            .OrderByDescending(k => k.Position.Y);

        float count = 0;

        foreach (Konpeito k in konpeitos)
        {
            if (k != null)
            {
                count += 0.05f;
                HitboxComponent hitbox = k.GetNode<HitboxComponent>("HitboxComponent");
                hitbox.SetDeferred(Area2D.PropertyName.Monitoring, false);
                hitbox.SetDeferred(Area2D.PropertyName.Monitorable, false);
                DelayHit(k, count);
            }
        }

    }

    private async void DelayHit(Konpeito k, float time)
    {
        await ToSignal(KonpeitoManager.GetInstance(this).GetTree().CreateTimer(time), "timeout");

        try
        {
            EventBus.Instance.Raise(new KonpeitoHitEvent(k, GetGroups(), 50));
            k.CallDeferred(Node.MethodName.QueueFree);
        }
        catch (ObjectDisposedException)
        {
            return;
        }
    }

    private void OnFinished()
    {
        QueueFree();
    }
}
