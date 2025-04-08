using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DND;
using Action = System.Action;

public partial class GameUi : Control
{
    [Export] private Label _locationName;
    [Export] private Label _locationDescription;
    [Export] private TextureRect _locationImage;

    [Export] private Label _eventName;
    [Export] private Label _eventDescription;

    private List<ActionButtons> _actionButtons;
    [Export] private Control _buttonsParent;
    [Export] private EnemyUI _enemyUi;
    [Export] private float _textSpawnSpeed;
    [Export] private BattleActionsController _battleActions;

    public override void _Ready()
    {
        _actionButtons = [];
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
        TypeText(@event.Description);
        for (var i = 0; i < _actionButtons.Count; i++)
        {
            _actionButtons[i].ChangeAction(i < @event.Actions.Count ? @event.Actions[i] : null);
        }
    }

    private async void TypeText(string text)
    {
        _eventDescription.Text = "";
        foreach (var letter in text)
        {
            _eventDescription.Text += letter;
            await Task.Delay((int)(_textSpawnSpeed * 1000));
        }
    }
    public void StartBattleMode()
    {
        _buttonsParent.Visible = false;
        _battleActions.Visible = true;
    }

    public void EndBattleMode()
    {
        _buttonsParent.Visible = true;
        _battleActions.Visible = false;
    }

    private void SwitchActionButtonsVisible(bool state)
    {
        foreach (var button in _actionButtons)
        {
            button.Visible = state;
        }
    }
}