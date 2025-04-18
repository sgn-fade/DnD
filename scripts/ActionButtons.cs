using Godot;
using System;
using DND;

public partial class ActionButtons : TextureButton
{
    [Export] private RichTextLabel _actionDescription;
    private DND.Action _linkedAction;

    public delegate void ActionPressed(DND.Action action);

    public static event ActionPressed OnActionPressed;

    public override void _Ready()
    {
        Pressed += OnButtonPressed;
    }

    public void ChangeAction(DND.Action newAction)
    {
        _linkedAction = newAction;
        Visible = _linkedAction != null;

        if (_linkedAction == null)
            return;

        UpdateActionDescription();
        HandleStat();
        HandleTag();
    }

    private void UpdateActionDescription()
    {
        _actionDescription.Text = _linkedAction.Description;
    }

    private void HandleStat()
    {
        var requiredStat = _linkedAction.RequiredStat;
        if (requiredStat?.Type != null)
        {
            var color = GetColorByStatType(requiredStat.Type);
            _actionDescription.AppendText(
                $"[color={color}][outline_size=80][{requiredStat.Value}][/outline_size][/color]");
        }
    }

    private void HandleTag()
    {
        var tag = _linkedAction.RequiredTag;
        if (string.IsNullOrEmpty(tag))
            return;

        if (!PlayerViewModel.Instance.CheckTag(tag))
        {
            Visible = false;
            return;
        }

        _actionDescription.AppendText($"[outline_size=80][{tag}][/outline_size]");
    }


    public void OnButtonPressed()
    {
        OnActionPressed?.Invoke(_linkedAction);
    }

    public String GetColorByStatType(String statType)
    {
        return statType switch
        {
            "strength" => "red",
            "dexterity" => "yellow",
            "constitution" => "green",
            "intelligence" => "blue",
            _ => "black"
        };
    }
}