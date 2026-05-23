using Godot;
using System;

public partial class DialogueManager : Node
{
	[Export] private RichTextLabel text;
	[Export] private RichTextLabel name;

	[Export] private Control dialogueContainer;

	[Export] private bool inDialogue = false;

	[Export] double writeSpeed = 1.0d;

	private double writeChars = 0.0d;

	private dialogueDataContainer actDialogue;
	private dialogueNode actNode;

	public void startDialogue(ref dialogueDataContainer aux){
		actDialogue = aux;
		actDialogue.resetDialogue();
		dialogueContainer.Visible = true;
		actNode = actDialogue.GetActDialogueNode();
		text.Text =   "[fade start=0 length=1000]" + actNode.getText();
		name.Text = actNode.getName();
		inDialogue = true;
		writeChars = 0;
		   //Use a signal to desactive de move input
	}
	public bool getInDialogue() => inDialogue;


	public override void _Process(double delta)
	{
		if (inDialogue)
		{
		   writeChars += writeSpeed*delta;
		   text.VisibleCharacters = writeChars;
			if (Input.IsActionJustPressed("moveDown"))
			{   
				if (actDialogue.endDialogue())
				{
				dialogueContainer.Visible = false;
				//Use a signal to active de move input
				}
				else
				{
					actDialogue.nextDialogueNode();
					actNode = actDialogue.GetActDialogueNode();
					text.Text =  "[fade start=0 length=10000]" + actNode.getText();
					name.Text = actNode.getName();
					writeChars = 0;
				}
			}
		}
	}
}
