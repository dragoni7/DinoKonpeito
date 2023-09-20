using Godot;
using System;

public partial class Konpeito : RigidBody2D
{
	public float Speed { get; set; }
	private void OnVisibleOnScreenNotifier2DScreenExited()
	{
		QueueFree();
	}

    public override void _Process(double delta)
	{
		this.MoveAndCollide(new Vector2(0, Speed));
	}
}
