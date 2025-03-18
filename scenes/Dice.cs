using System;
using Godot;

public partial class Dice : RigidBody3D
{
	public override void _Ready()
	{
		AngularVelocity = new Vector3(GD.RandRange(10, 20), GD.RandRange(10, 20), GD.RandRange(10, 20));
		BodyEntered += OnFloorEntered;
	}

	public void OnFloorEntered(Node body)
	{
		AngularVelocity = Vector3.Zero;
	}
}
