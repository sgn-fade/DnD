using Godot;
using System;

public partial class ActionButtons : TextureButton
{
    [Export] private Label _actionDescription;
    private DND.Action _linkedAction;

    public void ChangeAction(DND.Action newAction)
    {
        _linkedAction = newAction;
        if (_linkedAction != null)
            _actionDescription.Text = _linkedAction.Description;
        Visible = _linkedAction != null;
    }
}