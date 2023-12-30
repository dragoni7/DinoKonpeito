using Godot;

public partial class Menu : Node
{
	public void OnStartButtonPressed()
	{
		GetNode<SceneLoader>("/root/SceneLoader").ChangeToScene("Gameplay/Game.tscn");
	}

	public void OnOptionsButtonPressed()
	{

	}

	public void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}
}
