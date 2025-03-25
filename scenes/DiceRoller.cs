using Godot;
using System;

public partial class DiceRoller : Node3D
{
	[Export] private PackedScene _diceScene;
	private Dice _dice;

	public override void _Input(InputEvent @event)
	{
		if (Input.IsActionPressed("space"))
		{
			_dice?.QueueFree();
			_dice = _diceScene.Instantiate<Dice>();
			AddChild(_dice);
			_dice.GlobalPosition = new Vector3(0, 4, 0);
		}
	}

	public void RequestDiceRoll()
	{

	}

}
