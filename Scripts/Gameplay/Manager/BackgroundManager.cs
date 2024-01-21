using Godot;

public partial class BackgroundManager : Node, ISingleInstance<BackgroundManager>
{
    [Export]
    private PackedScene _backgroundScene;

    private Background _background;

    public override void _Ready()
    {
        _background = _backgroundScene.Instantiate<Background>();
        AddChild(_background);
        EventBus.Instance.Subscribe<DifficultyChangeEvent>(OnDifficultyChange);
    }

    public static BackgroundManager GetInstance(Node from)
    {
        return from.GetNode<BackgroundManager>("/root/Game/BackgroundManager");
    }

    public void OnDifficultyChange(DifficultyChangeEvent e)
    {
        if (_background.Layers.Count >= e.NewDifficulty)
        {
            _background.Layers[e.NewDifficulty - 1].Modulate = Colors.Transparent;
            _background.Layers[e.NewDifficulty - 1].Visible = true;
            GetTree().CreateTween().TweenProperty(_background.Layers[e.NewDifficulty - 1], "modulate", Colors.White, 1.5);

            ColorRect colorRect = _background.ColorLayer.GetNode<ColorRect>("ColorRect");
            GetTree().CreateTween().TweenProperty(colorRect, "color", colorRect.Color.Darkened(0.25f), 1);
        }
    }
}
