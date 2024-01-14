using Godot;
using System;

public partial class SceneLoader : Node, ISingletonNode
{
    [Export] private string _sceneFolder;

    public static T GetInstance<T>(Node from) where T : Node
    {
        return from.GetNode<T>("/root/SceneLoader");
    }

    public void ChangeToScene(string sceneName)
    {
        string f = "Scenes/";

        GetTree().ChangeSceneToFile($"res://{f}{sceneName}");
    }
}
