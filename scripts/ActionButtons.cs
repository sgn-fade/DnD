using Godot;
using System;

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
        if (_linkedAction != null)
        {
            _actionDescription.Text = _linkedAction.Description;
            if (_linkedAction.RequiredStat.Type != null)
            {
                var color = GetColorByStatType(_linkedAction.RequiredStat.Type);
                _actionDescription.AppendText($"[color={color}] [outline_size={80}]" +
                                              $"[{_linkedAction.RequiredStat.Value}]" +
                                              $"[/outline_size][/color]");
            }
        }
        Visible = _linkedAction != null;
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