using Godot;
using System;
using DND;

public partial class StatDisplay : Control
{
    [Export] private TextureButton _upgradeButton;
    [Export] private Label _value;
    [Export] private TextureRect _texture;
    [Export] private Color StrColor;
    [Export] private Color IntColor;
    [Export] private Color DexColor;
    [Export] private Color ConColor;
    [Export] public PlayerData.PlayerStats LinkedStat { get; set; }

    [Signal]
    public delegate void OnUpgradeStatPressedEventHandler(PlayerData.PlayerStats linkedStat);

    public override void _Ready()
    {
        SetColor();
    }

    public void ShowUpgrade()
    {
        _upgradeButton.Show();
    }

    public void OnUpgradePressed()
    {
        EmitSignalOnUpgradeStatPressed(LinkedStat);
    }

    public void ChangeTextValue(int value)
    {
        _value.Text = value.ToString();
    }
    public void HideUpgrade()
    {
        _upgradeButton.Hide();
    }

    private void SetColor()
    {
        _texture.Modulate = LinkedStat switch

        {
            PlayerData.PlayerStats.strength => StrColor,
            PlayerData.PlayerStats.dexterity => DexColor,
            PlayerData.PlayerStats.intelligence => IntColor,
            PlayerData.PlayerStats.constitution => ConColor,
            _ => Colors.HotPink,
        };
    }
}