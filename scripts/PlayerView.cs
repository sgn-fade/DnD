using Godot;
using System;
using DND;

public partial class PlayerView : Control
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

    public void UpdatePlayerView(PlayerData data)
    {
        _playerIcon.Texture = TextureStorage.Instance.GetPlayerIcon(data.Class);
        _playerName.Text = data.Name;

        _playerHealth.Text = $"HP: {data.Hp}/{data.MaxHp}";
        //_playerLevel.Text = $"Lvl: {player.Level}";

        _strength.Text = $"{data.Stats[0].Value}";
        _dexterity.Text = $"{data.Stats[1].Value}";
        _constitution.Text = $"{data.Stats[2].Value}";
        _intelligence.Text = $"{data.Stats[3].Value}";
    }

    public void DisplayLevelUp()
    {

    }
}
