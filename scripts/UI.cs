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
    [Export] private EnemyUI _enemyUi;

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
        _locationImage.Texture = TextureStorage.Instance.GetLocationBackground(location.Type);
    }

    public void ChangeEvent(Event @event)
    {
        _eventName.Text = @event.Name;
        _eventDescription.Text = @event.Description;
        for (var i = 0; i < _actionButtons.Count; i++)
        {
            _actionButtons[i].ChangeAction(i < @event.Actions.Count ? @event.Actions[i] : null);
        }
    }


    public EnemyUI StartBattleMode()
    {
        return _enemyUi;
    }

    private void SwitchActionButtonsVisible(bool state)
    {
        foreach (var button in _actionButtons)
        {
            button.Visible = state;
        }
    }
}