using Godot;

public partial class SplashScreen : ColorRect
{
	public override void _Ready()
    {
        GetNode<Timer>("Timer").Timeout += () => SceneLoader.GetInstance(this).ChangeToScene("UI/Menu");
    }
}
