using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
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

    [Export] private Label _goldCount;
    [Export] public StatDisplay[] StatsDisplays = [];


    public void UpdateXpStat(double currentExp, int maxExp)
    {
        _playerExp.Text = $"{currentExp}/{maxExp}";
        _xpBar.Value = currentExp / maxExp * 100;
    }

    public void UpdateLevel(int newLevel)
    {
        _playerLevel.Text = newLevel.ToString();
    }

    public void UpdateHpBar(double currentHp, double maxHp)
    {
        _playerHealth.Text = $"{currentHp} {maxHp}";
        _hpBar.Value = currentHp / maxHp * 100;
    }

    public void ShowUpgradeStatButtons()
    {
        foreach (var statDisplay in StatsDisplays)
        {
            statDisplay.ShowUpgrade();
        }
    }
    public void HideUpgradeStatButtons()
    {
        foreach (var statDisplay in StatsDisplays)
        {
            statDisplay.HideUpgrade();
        }
    }

    public void UpdateStats(List<Stat> stats)
    {
        foreach (var t in stats)
        {
            var stat = StatsDisplays.First(display => display.LinkedStat.ToString() == t.Type);
            stat.ChangeTextValue(t.Value);
        }
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