using Godot;
using System;
using DND;

public partial class SwitchButton : TextureButton
{
	[Export] private PlayerData.PlayerClasses _linkedClasses;
	public delegate void ButtonSwitched(PlayerData.PlayerClasses @class, SwitchButton button);

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
