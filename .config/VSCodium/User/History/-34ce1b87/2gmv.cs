using Godot;
using System;


public partial class PlayerMoveWrd : CharacterBody2D
{
	[Export] float speed = 100.0f;
	private Vector2 inputResult;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		inputResult = new Vector2(0,0);
		if (Input.IsActionPressed("moveRight"))
			{
				inputResult += new Vector2(1*speed,0);
			}
		if (Input.IsActionPressed("moveLeft"))
			{
				inputResult += new Vector2(-1*speed,0);
			}
		Velocity = inputResult;
		MoveAndSlide();
	}
}
