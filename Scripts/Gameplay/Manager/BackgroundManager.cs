using Godot;

public partial class BackgroundManager : Node, ISingleInstance<BackgroundManager>
{
    [Export]
    private PackedScene _backgroundScene;

    [Export]
    private ShaderMaterial _rainbowMaterial;

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
            GetTree().CreateTween().TweenProperty(_background.Layers[e.NewDifficulty - 1], "modulate", Colors.White, 2);
        }

        switch (e.NewDifficulty)
        {
            case 2:
                {
                    GetTree().CreateTween().TweenProperty(_background.ColorLayer, "modulate", SaturateColor(_background.ColorLayer.Modulate), 1);
                    break;
                }
            case 4:
                {
                    GetTree().CreateTween().TweenProperty(_background.ColorLayer, "modulate", SaturateColor(_background.ColorLayer.Modulate), 1);
                    break;
                }
            case 8:
                {
                    GetTree().CreateTween().TweenProperty(_background.ColorLayer, "modulate", _background.ColorLayer.Modulate.Darkened(0.5f), 1);
                    break;
                }
            case 12:
                {
                    _background.ColorLayer.GetNode<Sprite2D>("ColorRect").Material = _rainbowMaterial;
                    break;
                }
        }
    }

    private Color SaturateColor(Color color)
    {
        return Color.FromHsv(color.H, color.S += 0.5f, color.V);
    }
}
