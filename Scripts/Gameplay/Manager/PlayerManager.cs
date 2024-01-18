using Godot;

partial class PlayerManager : Node, ISingleInstance<PlayerManager>
{
    [Export]
    private PackedScene _playerScene;

    public Player Player { get; private set; }

    public static PlayerManager GetInstance(Node from)
    {
        return from.GetNode<PlayerManager>("/root/Game/PlayerManager");
    }

    public void SpawnPlayer(Vector2 position)
    {
        Player = _playerScene.Instantiate<Player>();
        Player.Position = position;
        AddChild(Player);
    }
}
