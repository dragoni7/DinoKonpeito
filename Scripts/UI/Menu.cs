using Godot;

public partial class Menu : Node
{
	public void OnStartButtonPressed()
	{
        SceneLoader.GetInstance(this).ChangeToScene("Gameplay/Game.tscn");
	}

	public void OnOptionsButtonPressed()
	{

	}

	public void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}
}
