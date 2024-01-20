using Godot;

public partial class ExitParticlesComponent : GpuParticles2D
{
    public override void _ExitTree()
    {
        Emitting = true;
    }
}
