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
		if (Input.IsActionPressed("moveUp"))
			{
				inputResult += new Vector2(1*speed,0);
			}

			Velocity = inputResult;
	
	   

	move_and_slide();
	}
}
