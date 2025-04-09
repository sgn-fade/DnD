using Godot;
using System;
using System.Collections.Generic;
using DND;

public partial class PlayerView : Control
{
    [Export] private TextureRect _playerIcon;
    [Export] private TextureRect _weaponIcon;
    [Export] private TextureProgressBar _hpBar;
    [Export] private TextureProgressBar _xpBar;
    [Export] private Label _playerName;
    [Export] private Label _playerHealth;
    [Export] private Label _playerExp;
    [Export] private Label _playerLevel;

    [Export] private Label _strength;
    [Export] private Label _dexterity;
    [Export] private Label _constitution;
    [Export] private Label _intelligence;
    [Export] private Label _goldCount;

    public void UpdateXpStat(double currentExp, int maxExp)
    {
        _playerExp.Text = $"{currentExp}/{maxExp}";
        _xpBar.Value = 
    }
    public void UpdateLevel(int newLevel)
    {
        _playerLevel.Text = newLevel.ToString();
    }

    public void UpdateHpBar(double currentHp, double maxHp)
    {
        _playerHealth.Text = $"{currentHp} {maxHp}";

    }
    public void ShowExpText()
    {
        _playerExp.Visible = true;
    }
    public void HideExpText()
    {
        _playerExp.Visible = false;
    }

    public void UpdateStats(List<Stat> stats)
    {
        _strength.Text = $"{stats[0].Value}";
        _dexterity.Text = $"{stats[1].Value}";
        _constitution.Text = $"{stats[2].Value}";
        _intelligence.Text = $"{stats[3].Value}";
    }
    public void UpdateAll(PlayerData data)
    {
        _playerIcon.Texture = TextureStorage.Instance.GetPlayerIcon(data.Class);
        _playerName.Text = data.Name;
        UpdateStats(data.Stats);
        UpdateHpBar(data.Hp, data.MaxHp);
        UpdateXpStat(data.CurrentXp, data.XpThresholds[data.Level]);
    }
}
