using Godot;
using System;

public partial class DescriptionPopUp : Control
{
    [Export] private Label _name;
    [Export] private Label _desc;

    public void DisplayText(string name, string description)
    {
        _name.Text = name;
        _desc.Text = description;
    }
}