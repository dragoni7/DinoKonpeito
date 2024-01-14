using Godot;

public partial class Menu : Node
{
	public void OnStartButtonPressed()
	{
        SceneLoader.GetInstance<SceneLoader>(this).ChangeToScene("Gameplay/Game.tscn");
	}

	public void OnOptionsButtonPressed()
	{

	}

	public void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}
}
