using Godot;
using System;

public partial class SceneLoader : Node
{
    [Export] private string _sceneFolder;
    public void ChangeToScene(string sceneName)
    {
        string f = "Scenes/";

        GetTree().ChangeSceneToFile($"res://{f}{sceneName}");
    }
}
