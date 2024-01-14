using Godot;

partial class PlayerManager : Node, ISingletonNode
{
    [Export]
    private PackedScene _playerScene;

    public Player Player { get; private set; }

    public static T GetInstance<T>(Node from) where T : Node
    {
        return from.GetNode<T>("/root/Game/PlayerManager");
    }

    public void SpawnPlayer(Vector2 position)
    {
        Player = _playerScene.Instantiate<Player>();
        Player.Position = position;
        AddChild(Player);
    }
}
