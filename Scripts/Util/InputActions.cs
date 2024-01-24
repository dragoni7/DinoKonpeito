
public static class InputActions
{
    public const string ACTION_MOVE_LEFT = "Left";
    public const string ACTION_MOVE_RIGHT = "Right";
    public const string ACTION_UP = "Up";
    public const string ACTION_ESCAPE = "Escape";

    public static string[] Actions { get; private set; } =
    {
        ACTION_MOVE_LEFT,
        ACTION_MOVE_RIGHT,
        ACTION_UP,
        ACTION_ESCAPE
    };
}
