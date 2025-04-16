using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace DND;

public partial class PlayerViewModel : Node
{
    public PlayerData PlayerData;
    public static PlayerViewModel Instance { get; set; }
    [Export] private PlayerView _playerView;
    [Export] private Skill[] WarriorSkills;
    [Export] private Skill[] RogueSkills;
    [Export] private Skill[] MageSkills;

    public override void _Ready()
    {
        Instance = this;
    }

    public override void _EnterTree()
    {
        foreach (var statDisplay in _playerView.StatsDisplays)
        {
            statDisplay.OnUpgradeStatPressed += OnUpgradeStatPressed;
        }
    }

    private void OnUpgradeStatPressed(PlayerData.PlayerStats linkedStat)
    {
        PlayerData.Stats.First(stat => stat.Type == linkedStat.ToString()).Value++;
        PlayerData.LevelUpPoints--;
        if (PlayerData.LevelUpPoints <= 0) _playerView.HideUpgradeStatButtons();
        UpdateDataView();
    }

    public void Init(PlayerData playerData)
    {
        PlayerData = playerData;
        PlayerData.AddSkill(WarriorSkills[0]);
        UpdateDataView();
    }

    public void AddXp(double value)
    {
        PlayerData.CurrentXp += value;
        while (PlayerData.Level < PlayerData.XpThresholds.Length &&
               PlayerData.CurrentXp >= PlayerData.XpThresholds[PlayerData.Level])
        {
            PlayerData.CurrentXp -= PlayerData.XpThresholds[PlayerData.Level];
            LevelUp();
        }

        _playerView.UpdateXpStat(PlayerData.CurrentXp, PlayerData.XpThresholds[PlayerData.Level]);
    }

    public void LevelUp()
    {
        PlayerData.Level++;
        PlayerData.LevelUpPoints++;
        _playerView.UpdateLevel(PlayerData.Level);
        _playerView.ShowUpgradeStatButtons();
        if (PlayerData.LevelsThatGivesSkill.Contains(PlayerData.Level))
        {
            PlayerData.AddSkill(WarriorSkills[PlayerData.Skills.Count]);
        }
    }

    public void TakeDamage(double damage)
    {
        PlayerData.Hp -= damage;
        if (PlayerData.Hp <= 0)
        {
            Die();
            return;
        }

        RefreshUi();
    }

    private void Die()
    {
        //TODO death screen implementation
    }

    public bool CheckStat(Stat stat, int additionalBuff = 0)
    {
        if (stat.Type == null) return true;
        return stat.Value <= PlayerData.GetPlayerStat(stat.Type) + additionalBuff;
    }

    private void UpdateDataView()
    {
        RefreshUi();
    }

    public double GetDamage()
    {
        return PlayerData.Damage;
    }

    public void HealHp(int value)
    {
        PlayerData.Hp += value;
        if (PlayerData.Hp > PlayerData.MaxHp) PlayerData.Hp = PlayerData.MaxHp;
        RefreshUi();
    }
    private void RefreshUi() => _playerView.UpdateAll(PlayerData);

}