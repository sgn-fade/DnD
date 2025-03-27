using System;
using System.Collections.Generic;
using Godot;

namespace DND;

public partial class PlayerViewModel : Node
{
    public PlayerData PlayerData;
    public static PlayerViewModel Instance { get; set; }
    [Export] private PlayerView _playerView;

    public override void _Ready()
    {
        Instance = this;
    }

    public void Init(PlayerData playerData)
    {
        PlayerData = playerData;
        UpdateDataView();
    }

    public void AddXp(int value)
    {
        PlayerData.CurrentXp += value;
        while (PlayerData.Level < PlayerData.XpThresholds.Length &&
               PlayerData.CurrentXp >=  PlayerData.XpThresholds[PlayerData.Level])
        {
            PlayerData.CurrentXp -=  PlayerData.XpThresholds[PlayerData.Level];
            LevelUp();
        }
        _playerView.UpdateXpStat(PlayerData.CurrentXp, PlayerData.XpThresholds[PlayerData.Level]);
    }

    public void LevelUp()
    {
        PlayerData.Level++;
        _playerView.UpdateLevel(PlayerData.Level);
    }
    public void TakeDamage(int damage)
    {
        PlayerData.Hp -= damage;
        if (PlayerData.Hp <= 0)
        {
            Die();
            return;
        }
        _playerView.UpdateAll(PlayerData);
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
        _playerView.UpdateAll(PlayerData);
    }
}