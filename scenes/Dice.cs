using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;

public partial class Dice : RigidBody3D
{
	private List<DiceLabel> _labels;
	[Export] private float _rollStrength;
	public override void _Ready()
	{
		Sleeping = false;
		Freeze = false;
		LinearVelocity = Vector3.Zero;
		AngularVelocity = Vector3.Zero;
		Transform = Transform with
		{
			Basis = Transform.Basis * new Basis(Vector3.Right, (float)(GD.Randf() * Math.PI * 2))
		};
		Transform = Transform with
		{
			Basis = Transform.Basis * new Basis(Vector3.Up, (float)(GD.Randf() * Math.PI * 2))
		};
		Transform = Transform with
		{
			Basis = Transform.Basis * new Basis(Vector3.Forward, (float)(GD.Randf() * Math.PI * 2))
		};
		_labels = new List<DiceLabel>();
		foreach (var node in GetChildren())
		{
			if (node is DiceLabel label)
			{
				_labels.Add(label);
			}
		}

		var throwVector = new Vector3(GD.Randf() * 2 - 1,0, GD.Randf() * 2 - 1);
		AngularVelocity = throwVector * _rollStrength / 2;
		ApplyCentralImpulse(throwVector * _rollStrength);
		SleepingStateChanged += OnSleepingStateChanged;
	}

	private void OnSleepingStateChanged()
	{
		if (Sleeping)
		{
			foreach (var label in _labels)
			{
				if (label.Ray.IsColliding())
				{
					ActiveLabelByOppositeValue(label.OppositeValue);
				}
			}
		}
	}

	private void ActiveLabelByOppositeValue(int oppositeValue)
	{
		GD.Print("find " + oppositeValue);
		foreach (var label in _labels)
		{

			GD.Print("has " + label.Text.Text);
			if (oppositeValue.ToString() == label.Text.Text)
			{
				GD.Print("complete " + oppositeValue);
				label.Active();
				return;
			}
		}
	}

}
