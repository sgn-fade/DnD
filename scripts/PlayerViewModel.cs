using System;
using System.Collections.Generic;
using Godot;

namespace DND;

public partial class PlayerViewModel : Node
{
    public PlayerData PlayerData;
    public static PlayerViewModel Instance { get; set; }
    [Export] private PlayerView _playerView;

    public void Init(PlayerData playerData)
    {
        PlayerData = playerData;
        UpdateDataView();
    }

    public void AddXp(int value)
    {
        PlayerData.CurrentXp += value;
        while ( PlayerData.CurrentXp >  PlayerData.MaxXp)
        {
            LevelUp();
            PlayerData.CurrentXp -=  PlayerData.MaxXp;
        }
    }

    public void LevelUp()
    {
        PlayerData.Level++;
    }
    public void TakeDamage(int damage)
    {
        PlayerData.Hp -= damage;
        if (PlayerData.Hp <= 0) Die();
    }
    private void Die()
    {
        //TODO death screen implementation
    }
    public bool CheckStat(Stat stat)
    {
        if (stat.Type == null) return true;
        return stat.Value <= PlayerData.GetPlayerStat(stat.Type);
    }

    private void UpdateDataView()
    {
        _playerView.UpdatePlayerView(PlayerData);
    }
}