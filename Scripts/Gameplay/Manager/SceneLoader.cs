using Godot;

public partial class SceneLoader : Node, ISingletonNode<SceneLoader>
{
    [Export] private string _sceneFolder;

    public static SceneLoader GetInstance(Node from)
    {
        return from.GetNode<SceneLoader>("/root/SceneLoader");
    }

    public void ChangeToScene(string sceneName)
    {
        string f = "Scenes/";

        GetTree().ChangeSceneToFile($"res://{f}{sceneName}");
    }
}
