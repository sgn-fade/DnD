using Godot;
using System;

public partial class SoundButton : TextureButton
{
    [Export] private AudioStreamPlayer _player;
    public override void _Ready()
    {
        Pressed += OnPressed;
    }

    private void OnPressed()
    {
        _player.Play();
    }
}
