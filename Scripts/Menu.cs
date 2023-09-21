using Godot;

public partial class Menu : Node
{
	public void OnStartButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/Gameplay/Main.tscn");
	}

	public void OnOptionsButtonPressed()
	{

	}

	public void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}
}
