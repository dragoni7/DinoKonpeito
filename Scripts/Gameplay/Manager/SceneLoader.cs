using Godot;

public partial class SceneLoader : Node, ISingleInstance<SceneLoader>
{
    [Export] private string _sceneFolder;

    public static UIManager GetInstance(Node from)
    {
        return from.GetNode<UIManager>("/root/SceneLoader");
    }

    public void ChangeToScene(string sceneName)
    {
        string f = "Scenes/";

        GetTree().ChangeSceneToFile($"res://{f}{sceneName}");
    }
}
