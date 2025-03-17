using Godot;
using System;
using DND;

public partial class SwitchButton : TextureButton
{
	[Export] private Player.PlayerClasses _linkedClasses;
	public delegate void ButtonSwitched(Player.PlayerClasses @class, SwitchButton button);

	public static event ButtonSwitched OnButtonSwitched;
	public override void _Ready()
	{
		Pressed += OnPressed;
	}

	private void OnPressed()
	{
		Disabled = true;
		OnButtonSwitched?.Invoke(_linkedClasses, this);
	}
}
