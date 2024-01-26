using Godot;

public partial class Menu : Node
{
	public void OnStartButtonPressed()
	{
		GameStateManager.GetInstance(this).ChangeToState(GameState.Playing);
	}

	public void OnOptionsButtonPressed()
	{

	}

	public void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}
}
