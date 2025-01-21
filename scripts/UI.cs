using Godot;
using System;
using System.Collections.Generic;
using DND;

using Action = System.Action;

public partial class UI : CanvasLayer
{
    [Export] private Label _locationName;
    [Export] private Label _locationDescription;
    [Export] private TextureRect _locationImage;

    [Export] private Label _eventName;
    [Export] private Label _eventDescription;

    private List<ActionButtons> _actionButtons;
    [Export] private Control _buttonsParent;
    public override void _Ready()
    {
        _actionButtons = new List<ActionButtons>();
        foreach (var node in _buttonsParent.GetChildren())
        {
            if (node is ActionButtons buttons)
            {
                _actionButtons.Add(buttons);
            }
        }
    }


    public void ChangeLocation(Location location)
    {
        _locationName.Text = location.Name;
        _locationDescription.Text = location.Description;
        _locationImage.Texture = location.Background;
    }

    public void ChangeEvent(Event @event)
    {
        _eventName.Text = @event.Name;
        _eventDescription.Text = @event.Description;
        GD.Print(_actionButtons.Count);
        GD.Print(@event.Actions.Count);
        for (int i = 0; i < @event.Actions.Count; i++)
        {
            _actionButtons[i]._linkedAction = @event.Actions[i];
            _actionButtons[i].ChangeAction();
        }

        if (_actionButtons.Count > @event.Actions.Count)
        {
            for (int i = 2; i > @event.Actions.Count - 1; i--)
            {
                _actionButtons[i].Visible = false;
            }
        }
    }
}