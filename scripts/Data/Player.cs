using System;
using System.Collections.Generic;
using Godot;

namespace DND;

public partial class Player : Node
{
    public enum PlayerClasses
    {
        Warrior,
        Rogue,
        Mage,
    }
    public new string Name { get; set; }
    public PlayerClasses Class { get; set; }
    public List<Stat> Stats { get; set; }
    public int Hp { get; set; }
    public int MaxHp { get; set; }
    public int Damage { get; set; }
    public int CurrentXp { get; set; }
    public int MaxXp { get; set; }
    public int Level { get; set; }

    public static Player Instance { get; set; }

    public delegate void PlayerDataUpdated();
    public static  event PlayerDataUpdated OnPlayerDataUpdated;
    public override void _Ready()
    {
        Stats = new List<Stat>();
        Instance = this;
        Stats.Add(new Stat("strength", 10));
        Stats.Add(new Stat("dexterity", 10));
        Stats.Add(new Stat("constitution", 10));
        Stats.Add(new Stat("intelligence", 10));
        Class = PlayerClasses.Warrior;
    }

    public void Start()
    {
        OnPlayerDataUpdated?.Invoke();
    }
    public void GeneratePlayerStats()
    {

    }

    public void AddXp(int value)
    {
        CurrentXp += value;
        while (CurrentXp > MaxXp)
        {
            LevelUp();
            CurrentXp -= MaxXp;
        }
    }

    public void LevelUp()
    {
        Level++;
    }

    public int GetPlayerStat(String type)
    {
        return Stats.Find(s => s.Type == type).Value;
    }
    public override string ToString()
    {
        var playerStatsSummary = Stats != null
            ? string.Join("\n", Stats)
            : "None";
        
        return $"Player stats: {playerStatsSummary}\n\r";
    }

    public void TakeDamage(int damage)
    {
        Hp -= damage;
        if (Hp <= 0)
        {
            Die();
        }
    }

    
    private void Die()
    {
        throw new NotImplementedException();
    }
}