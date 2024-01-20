using Godot;
using Godot.Collections;
using System;
using System.Linq;

[Tool]
partial class KonpeitoManager : Node, ISingleInstance<KonpeitoManager>
{

    [Signal]
    public delegate void IncreaseScoreEventHandler(int amount);

    [Signal]
    public delegate void DisplayScoreTextEventHandler(int amount, Vector2 position);

    public float SpeedModifier { get; set; }

    public static KonpeitoManager GetInstance(Node from)
    {
        return from.GetNode<KonpeitoManager>("/root/Game/KonpeitoManager");
    }

    public override void _Ready()
    {
        SpeedModifier = 1;
    }

    public override void _PhysicsProcess(double delta)
    {
        GetTree().CallGroup("Konpeito", Konpeito.MethodName.Move, SpeedModifier);
    }

    public void OnKonpeitoDestroyed(Konpeito konpeito, Array<StringName> groups)
    {
        bool noPoints = groups.Contains("Floor");

        if (!noPoints)
        {
            KonpeitoEffect effect = konpeito.Effect;
            effect.Reparent(this);
            effect.Execute();

            int score = GetKonpeitoScore(konpeito.Position);

            EmitKonpeitoScore(konpeito.Position, score);
        }

        konpeito.QueueFree();
    }

    public void HitAllKonpeito()
    {
        var konpeitos = GetTree().GetNodesInGroup("Konpeito")
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
        await ToSignal(GetTree().CreateTimer(time), "timeout");

        try
        {
            EmitKonpeitoScore(k.Position, 50);
            k.CallDeferred(Node.MethodName.QueueFree);
        }
        catch (ObjectDisposedException)
        {
            return;
        }
    }

    private int GetKonpeitoScore(Vector2 pos)
    {
        float distance = pos.Y;

        switch(distance) {

            case < 250:
                {
                    return (int)GameConsts.Scores.Highest;
                }
            case < 450:
                {
                    return (int)GameConsts.Scores.High;
                }
            case < 600:
                {
                    return (int)GameConsts.Scores.Mid;
                }
            case < 800:
                {
                    return (int)GameConsts.Scores.Low;
                }
            default:
                {
                    return (int)GameConsts.Scores.Lowest;
                }
        }
    }

    private void EmitKonpeitoScore(Vector2 position, int score)
    {
        EmitSignal(SignalName.IncreaseScore, score);
        EmitSignal(SignalName.DisplayScoreText, score, position);
    }

}
