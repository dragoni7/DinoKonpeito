using Godot;

[GlobalClass]
public partial class RandomKonpeito : Resource
{
    [Export]
    public string Name { get; private set; }

    [Export]
    public PackedScene KonpeitoScene { get; private set; }

    [Export]
    public int PickChance { get; private set; } = 1;

    [Export]
    public bool DefaultPicked { get; set; } = true;
}
