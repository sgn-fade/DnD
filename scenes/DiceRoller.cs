using Godot;
using System;

public partial class DiceRoller : Node3D
{
	[Export] private PackedScene _diceScene;
	private Dice _dice;
	[Signal]
	public delegate void DiceRolledEventHandler(int value);

	public void RollDice()
	{
		_dice?.QueueFree();
		_dice = _diceScene.Instantiate<Dice>();
		AddChild(_dice);
		_dice.GlobalPosition = new Vector3(0, 4, 0);
		_dice.RollEnded += RollEnded;
	}

	private void RollEnded(int value)
	{
		EmitSignal(SignalName.DiceRolled, value);
	}
}
