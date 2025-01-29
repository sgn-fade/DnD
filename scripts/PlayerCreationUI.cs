using Godot;
using System;

public partial class PlayerCreationUI : CanvasLayer
{
    //Todo ontoggle another buttons when 1 toggled
    [Export] private Label _classDescription;
    
    [Export] private TextureButton[] _textureButtons;
    
    private void OnWarriorButtonToggle(bool toggleOn)
    {
        if (toggleOn)
        {
            _textureButtons[0].Disabled = true;
            foreach (var button in _textureButtons)
            {
                if (button != _textureButtons[0])
                {
                    button.ButtonPressed = false;
                    button.Disabled = false;
                }
            }
            _classDescription.Text = "alolaloal";

        }
        else
            _classDescription.Text = "";
    }
    private void OnRogueButtonToggle(bool toggleOn)
    {
        if (toggleOn)
        {
            _textureButtons[1].Disabled = true;
            foreach (var button in _textureButtons)
            {
                if (button != _textureButtons[1])
                {
                    button.ButtonPressed = false;
                    button.Disabled = false;

                }
            }
            _classDescription.Text = "2222222";

        }
        else
            _classDescription.Text = "";
    }
    private void OnMageButtonToggle(bool toggleOn)
    {
        if (toggleOn)
        {
            _textureButtons[2].Disabled = true;
            foreach (var button in _textureButtons)
            {
                if (button != _textureButtons[2])
                {
                    button.ButtonPressed = false;
                    button.Disabled = false;

                }
            }
            _classDescription.Text = "333333333";

        }
        else
            _classDescription.Text = "";
    }
}
