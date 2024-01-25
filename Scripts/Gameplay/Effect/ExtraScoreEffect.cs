using Godot;

[Tool]
public partial class ExtraScoreEffect : KonpeitoEffect
{
    public override void Execute()
    {
        float count = 0;
        int amount = GD.RandRange(1, 5);

        for (int i = 0; i < amount; i++)
        {
            count += 0.2f;
            DelayScore(count);
        }

        Finish(count);
    }

    private async void DelayScore(float time)
    {
        await ToSignal(KonpeitoManager.GetInstance(this).GetTree().CreateTimer(time), "timeout");

        GameManager.GetInstance(this).Score += (int)GameConsts.Scores.Lower;
        Vector2 position = Position + VectorUtil.RandPointInCircle(20f);
        UIManager.GetInstance(this).SpawnFloatingText(GameConsts.Scores.Lower, position);
    }

    private async void Finish(float time)
    {
        await ToSignal(KonpeitoManager.GetInstance(this).GetTree().CreateTimer(time), "timeout");
        QueueFree();
    }
}
