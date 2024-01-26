using Godot;

public partial class GameStateManager : Node, ISingleInstance<GameStateManager>
{
    public GameState CurrentState { get; private set; } = GameState.Menu;

    public static GameStateManager GetInstance(Node from)
    {
        return from.GetNode<GameStateManager>("/root/GameStateManager");
    }

    public void ChangeToState(GameState newState)
    {
        if (newState != CurrentState)
        {
            SceneLoader sceneLoader = SceneLoader.GetInstance(this);
            UIManager uiManager = UIManager.GetInstance(this);
            AudioManager audioManager = AudioManager.GetInstance(this);

            switch (newState)
            {
                case GameState.Menu:
                    {
                        sceneLoader.ChangeToScene("UI/Menu");

                        CurrentState = newState;

                        break;
                    }
                case GameState.Paused:
                    {
                        CurrentState = newState;
                        break;
                    }
                case GameState.Playing:
                    {
                        sceneLoader.ChangeToScene("Gameplay/Game");

                        audioManager.SubscribeEvents();
                        uiManager.SubscribeEvents();

                        uiManager.ShowHUD(true);
                        uiManager.ShowMessage("Begin!");

                        CurrentState = newState;

                        break;
                    }
                case GameState.GameOver:
                    {
                        uiManager.ShowGameOver();

                        GameManager.GetInstance(this).UpdateHighScore();
                        GameData.Instance.SaveData();
                        CurrentState = newState;

                        ChangeToState(GameState.ExitingGame);

                        break;
                    }
                case GameState.ExitingGame:
                    {
                        GetNode<KonpeitoSpawner>("/root/Game/KonpeitoSpawner").Reset();
                        uiManager.ShowHUD(false);
                        KonpeitoManager.GetInstance(this).SpeedModifier = 1;

                        EventBus.Instance.UnsubscribeAll();

                        PlayerManager.GetInstance(this).DespawnPlayer();
                        KonpeitoManager.GetInstance(this).ClearKonpeito();

                        CurrentState = newState;

                        ChangeToState(GameState.Menu);

                        break;
                    }
            }
        }
    }
}

public enum GameState
{
    Playing,
    GameOver,
    ExitingGame,
    Paused,
    Menu
}
