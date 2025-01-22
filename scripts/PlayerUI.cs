using Godot;
using System;
using DND;

public partial class PlayerUI : Control
{
    [Export] private TextureRect _playerIcon;
    [Export] private TextureRect _weaponIcon;
    [Export] private Label _playerName;
    [Export] private Label _playerHealth;
    [Export] private Label _playerLevel;

    [Export] private Label _strength;
    [Export] private Label _dexterity;
    [Export] private Label _constitution;
    [Export] private Label _intelligence;
    [Export] private Label _goldCount;

    public override void _Ready()
    {
        Player.OnPlayerDataUpdated += UpdatePlayerView;
    }

    public void UpdatePlayerView()
    {
        var player = Player.Instance;
        _playerIcon.Texture = GetPlayerIcon(player.Class);
        _playerName.Text = player.Name;

        _playerHealth.Text = $"HP: {player.Hp}/{player.MaxHp}";
        //_playerLevel.Text = $"Lvl: {player.Level}";

        _strength.Text = $"{player.Stats[0].Value}";
        _dexterity.Text = $"{player.Stats[1].Value}";
        _constitution.Text = $"{player.Stats[2].Value}";
        _intelligence.Text = $"{player.Stats[3].Value}";

    }

    public Texture2D GetPlayerIcon(Player.PlayerClasses @class)
    {
        return @class switch
        {
            Player.PlayerClasses.Warrior => TextureStorage.Instance.Warrior,
            Player.PlayerClasses.Rogue => TextureStorage.Instance.Rogue,
            Player.PlayerClasses.Mage => TextureStorage.Instance.Mage,
            _ => throw new ArgumentOutOfRangeException(nameof(@class), @class, null)
        };
    }
}
