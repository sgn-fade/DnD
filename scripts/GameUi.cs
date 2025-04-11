using Godot;
using System;
using System.Collections.Generic;
using System.Threading;
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
    [Export] private RichTextLabel _battleLog;

    private List<ActionButtons> _actionButtons;
    [Export] private Control _buttonsParent;
    [Export] private Control _storyGroup;
    [Export] private Control _battleGroup;
    [Export] private EnemyUI _enemyUi;
    [Export] private float _textSpawnSpeed;
    [Export] private BattleActionsController _battleActions;
    private CancellationTokenSource _typingCancellation;

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

    public void PushToBattleLog(string text)
    {
        _battleLog.Text += text;
    }
    private async void TypeText(string text)
    {
        _typingCancellation?.Cancel();
        _typingCancellation = new CancellationTokenSource();
        var token = _typingCancellation.Token;

        _eventDescription.Text = "";
        try
        {
            foreach (var letter in text)
            {
                _eventDescription.Text += letter;
                await Task.Delay(TimeSpan.FromSeconds(_textSpawnSpeed), token);
            }
        }
        catch (TaskCanceledException)
        {
            // text interrupt
        }
    }
    public void StartBattleMode()
    {
        _storyGroup.Visible = false;
        _battleGroup.Visible = true;
        _battleActions.Visible = true;
    }

    public void EndBattleMode()
    {
        _storyGroup.Visible = true;
        _battleGroup.Visible = false;
        _battleActions.Visible = false;
    }

    private void SwitchActionButtonsVisible(bool state)
    {
        _actionButtons.ForEach(button => button.Visible = state);
    }
}