using Godot;
using System;

public partial class ActionButtons : TextureButton
{
    [Export] private Label _actionDescription;
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
            _actionDescription.Text = _linkedAction.Description;
        Visible = _linkedAction != null;
    }

    public void OnButtonPressed()
    {
        OnActionPressed?.Invoke(_linkedAction);
    }
}