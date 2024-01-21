using Godot;

public partial class ParticleManager : Node, ISingleInstance<ParticleManager>
{

    [Export]
    PackedScene _konpeitoDestroyScene;

    public static ParticleManager GetInstance(Node from)
    {
        return from.GetNode<ParticleManager>("/root/Game/ParticleManager");
    }

    public override void _Ready()
    {
        EventBus.Instance.Subscribe<KonpeitoHitEvent>(OnKonpeitoHit);
    }

    public void OnKonpeitoHit(KonpeitoHitEvent e)
    {
        Color color = e.KonpeitoHit.GetNode<Sprite2D>("Sprite2D").Modulate;
        Vector2 position = e.KonpeitoHit.Position;

        GpuParticles2D particles = _konpeitoDestroyScene.Instantiate<GpuParticles2D>();
        particles.Position = position;
        particles.Modulate = color;
        AddChild(particles);
        particles.Emitting = true;
        particles.Finished += particles.QueueFree;
    }
}
