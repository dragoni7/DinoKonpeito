using Godot;
using Godot.Collections;

public partial class ParticleManager : Node, ISingleInstance<ParticleManager>
{
    public static ParticleManager GetInstance(Node from)
    {
        return from.GetNode<ParticleManager>("/root/Game/ParticleManager");
    }

    [Export]
    PackedScene _konpeitoDestroyScene;

    public void OnKonpeitoDestroyed(Konpeito konpeito, Array<StringName> groups)
    {
        Color color = konpeito.Modulate;
        Vector2 position = konpeito.Position;

        GpuParticles2D particles = _konpeitoDestroyScene.Instantiate<GpuParticles2D>();
        particles.Position = position;
        particles.Modulate = color;
        AddChild(particles);
        particles.Emitting = true;

    }
}
