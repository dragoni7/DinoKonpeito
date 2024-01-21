using Godot;

[Tool]
partial class KonpeitoManager : Node, ISingleInstance<KonpeitoManager>
{
    public float SpeedModifier { get; set; }

    public static KonpeitoManager GetInstance(Node from)
    {
        return from.GetNode<KonpeitoManager>("/root/Game/KonpeitoManager");
    }

    public override void _Ready()
    {
        SpeedModifier = 1;
        EventBus.Instance.Subscribe<KonpeitoHitEvent>(OnKonpeitoHit, EventPriority.Low);
    }

    public override void _PhysicsProcess(double delta)
    {
        GetTree().CallGroup("Konpeito", Konpeito.MethodName.Move, SpeedModifier);
    }

    public void OnKonpeitoHit(KonpeitoHitEvent e)
    {
        bool doEffect = !(e.GroupsHit.Contains("Floor") || e.GroupsHit.Contains("Effect"));

        if (doEffect)
        {
            KonpeitoEffect effect = e.KonpeitoHit.Effect;
            effect.Reparent(this);
            effect.Execute();
        }

        e?.KonpeitoHit.QueueFree();
    }
}
