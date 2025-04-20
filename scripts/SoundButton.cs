using Godot;
using System;

public partial class SoundButton : TextureButton
{
    [Export] protected AudioStreamPlayer _defaultAudioPlayer;
    public override void _Ready()
    {
        Pressed += OnPressed;
    }

    protected virtual void OnPressed()
    {
        _defaultAudioPlayer.Play();
    }
}
