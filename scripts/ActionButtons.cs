using Godot;
using System;

public partial class ActionButtons : TextureButton
{
	[Export] private Label _actionDescription;
	public DND.Action _linkedAction;
	
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}

	public void ChangeAction()
	{
		_actionDescription.Text = _linkedAction.Description;
	}
}
