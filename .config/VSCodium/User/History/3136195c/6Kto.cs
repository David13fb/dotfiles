using Godot;
using System;

public partial class dialogueStartTriger : Node
{
	// Called when the node enters the scene tree for the first time.
	[Export] DialogueManager _mdm;
	[Export] dialogueDataContainer _mdata;

	[Export] bool repeat = false;

	[Export] bool done = false;
	public override void _Ready()
	{
		_mdata.startDataGen();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	//TODO hay que hacerlo con un trigger y esas cosas
	public override void _Process(double delta)
	{
		if (!_mdm.getInDialogue() && done == false)
		{
			if (Input.IsActionPressed("moveUp"))
			{
				_mdm.startDialogue(ref _mdata);
				if(!repeat) done = true;
			}
		}
	}
}
