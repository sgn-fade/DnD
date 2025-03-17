using Godot;
using System;
using DND;

public partial class PlayerCreationUI : CanvasLayer
{
    [Export] private Label _classDescription;
    [Export] private TextureRect _classIcon;
    
    private SwitchButton _currentButton;
    
    public override void _Ready()
    {
        SwitchButton.OnButtonSwitched += OnButtonSwitched;
    }

    private void OnButtonSwitched(PlayerData.PlayerClasses @class, SwitchButton button)
    {
        if (_currentButton != null)
        {
            _currentButton.Disabled = false;
            _currentButton.ButtonPressed = false;
        }
        _currentButton = button;
        _classDescription.Text = GetClassDescription(@class);
        _classIcon.Texture = TextureStorage.Instance.GetPlayerIcon(@class);
    }

    public override void _ExitTree()
    {
        SwitchButton.OnButtonSwitched -= OnButtonSwitched;
    }
    
    public String GetClassDescription(PlayerData.PlayerClasses @class)
    {
        return @class switch
        {
            PlayerData.PlayerClasses.Warrior => "Warrior",
            PlayerData.PlayerClasses.Rogue => "Rogue",
            PlayerData.PlayerClasses.Mage => "Mage",
            _ => throw new ArgumentOutOfRangeException(nameof(@class), @class, null)
        };
    }
}