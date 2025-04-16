using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;

public partial class Dice : RigidBody3D
{
	private List<DiceLabel> _labels;
	[Export] private float _rollStrength;
	public System.Action<int> RollEnded;
	public override void _Ready()
	{
		Sleeping = false;
		Freeze = false;
		LinearVelocity = Vector3.Zero;
		AngularVelocity = Vector3.Zero;
		ApplyRandomRotation();
		_labels = [];
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
	private void ApplyRandomRotation()
	{
		var random = (float angle, Vector3 axis) => new Basis(axis, angle);

		var basis = Transform.Basis;
		basis *= random(GD.Randf() * MathF.PI * 2, Vector3.Right);
		basis *= random(GD.Randf() * MathF.PI * 2, Vector3.Up);
		basis *= random(GD.Randf() * MathF.PI * 2, Vector3.Forward);

		Transform = Transform with { Basis = basis };
	}
	private void OnSleepingStateChanged()
	{
		if (Sleeping)
		{
			foreach (var label in _labels.Where(label => label.Ray.IsColliding()))
			{
				ActiveLabelByOppositeValue(label.OppositeValue);
			}
		}
	}

	private void ActiveLabelByOppositeValue(int oppositeValue)
	{
		foreach (var label in _labels.Where(label => oppositeValue.ToString() == label.Text.Text))
		{
			label.Active();
			RollEnded?.Invoke(oppositeValue);
			return;
		}
	}

}
