using Godot;
using System;

public partial class ArrowButton : Control
{
    [Export] public Label NameStat;
    [Export] public Label StatValueText;
    [Export] public TextureButton Left;
    [Export] public TextureButton Right;
    private int _statValue = 10;

    public override void _EnterTree()
    {
        Left.Pressed += OnLeftButtonPressed;
        Right.Pressed += OnRightButtonPressed;
    }

    public override void _ExitTree()
    {
        Left.Pressed -= OnLeftButtonPressed;
        Right.Pressed -= OnRightButtonPressed;
    }

    public override void _Ready()
    {
        UpdateStatText();
    }

    public void OnLeftButtonPressed()
    {
        _statValue--;
        UpdateStatText();
        if (_statValue == 1)
        {
            Left.Disabled = true;
        }
    }

    public void OnRightButtonPressed()
    {
        _statValue++;
        UpdateStatText();
        Left.Disabled = false;
    }

    public void UpdateStatText()
    {
        StatValueText.Text = _statValue.ToString();
    }
}