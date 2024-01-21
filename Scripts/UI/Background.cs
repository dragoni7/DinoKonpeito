using Godot;
using Godot.Collections;

public partial class Background : ParallaxBackground
{
    [Export]
    public ParallaxLayer ColorLayer { get; private set; }

    [Export]
    public Array<ParallaxLayer> Layers { get; private set; }
}
