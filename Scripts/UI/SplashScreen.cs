using Godot;

public partial class SplashScreen : ColorRect
{
	public override void _Ready()
    {
        GameData.Instance.ReadData();
        GetNode<Timer>("Timer").Timeout += () => SceneLoader.GetInstance(this).ChangeToScene("UI/Menu");
    }
}
