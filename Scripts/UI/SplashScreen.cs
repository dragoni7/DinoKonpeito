using Godot;
using System;

public partial class SplashScreen : ColorRect
{
	public override void _Ready()
    {
        GetNode<Timer>("Timer").Timeout += () => SceneLoader.GetInstance<SceneLoader>(this).ChangeToScene("UI/Menu.tscn");
    }
}
