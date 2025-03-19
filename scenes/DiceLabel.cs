using Godot;
using System;

public partial class DiceLabel : Node3D
{
	public RayCast3D Ray;
	[Export] public Label3D Text;
	[Export] public int OppositeValue;
	public override void _Ready()
	{
		Ray = GetNode<RayCast3D>("Node3D");
		Ray.TargetPosition = new Vector3(0, 0.05f, 0);
		Ray.HitFromInside = true;
	}

	public void Active()
	{
		Text.Modulate = Colors.Yellow;
	}
}
