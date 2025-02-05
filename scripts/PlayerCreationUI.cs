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

    private void OnButtonSwitched(Player.PlayerClasses @class, SwitchButton button)
    {
        if (_currentButton != null)
        {
            _currentButton.Disabled = false;
            _currentButton.ButtonPressed = false;
        }
        _currentButton = button;
        _classDescription.Text = GetClassDescription(@class);
        _classIcon.Texture = GetClassIcon(@class);
    }

    public override void _ExitTree()
    {
        SwitchButton.OnButtonSwitched -= OnButtonSwitched;
    }
    
    public Texture2D GetClassIcon(Player.PlayerClasses @class)
    {
        return @class switch
        {
            Player.PlayerClasses.Warrior => TextureStorage.Instance.Warrior,
            Player.PlayerClasses.Rogue => TextureStorage.Instance.Rogue,
            Player.PlayerClasses.Mage => TextureStorage.Instance.Mage,
            _ => throw new ArgumentOutOfRangeException(nameof(@class), @class, null)
        };
    }
    
    public String GetClassDescription(Player.PlayerClasses @class)
    {
        return @class switch
        {
            Player.PlayerClasses.Warrior => "Warrior",
            Player.PlayerClasses.Rogue => "Rogue",
            Player.PlayerClasses.Mage => "Mage",
            _ => throw new ArgumentOutOfRangeException(nameof(@class), @class, null)
        };
    }
}