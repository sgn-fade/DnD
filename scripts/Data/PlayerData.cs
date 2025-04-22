using System;
using System.Collections.Generic;
using System.Linq;

namespace DND;

public class PlayerData
{
    public enum PlayerClasses
    {
        Warrior,
        Rogue,
        Mage,
    }

    public string Name { get; set; } = "unknown";
    public PlayerClasses Class { get; set; } = PlayerClasses.Warrior;

    public enum PlayerStats
    {
        strength,
        dexterity,
        constitution,
        intelligence
    }
    public List<Stat> Stats { get; set; } =
    [
        new(PlayerStats.strength.ToString(), 10),
        new(PlayerStats.dexterity.ToString(), 9),
        new(PlayerStats.constitution.ToString(), 8),
        new(PlayerStats.intelligence.ToString(), 7)
    ];

    public PlayerData()
    {
        Hp = MaxHp;
    }

    public double Hp { get; set; }
    public double MaxHp => GetPlayerStat(PlayerStats.constitution.ToString()) * 10;
    public double Damage => Math.Round(GetPlayerStat(PlayerStats.strength.ToString()) / 4.0, 1);
    public double ChanceToDodgeAttack => GetPlayerStat(PlayerStats.dexterity.ToString()) * 0.02;
    public double CurrentXp { get; set; }
    public readonly int[] XpThresholds = [0, 10, 50, 100, 150, 300, 500, 850, 1100, 1500];
    public int Level { get; set; } = 1;
    public int LevelUpPoints { get; set; }

    public int GetPlayerStat(String type)
    {
        return Stats.Find(s => s.Type.ToString() == type).Value;
    }

    public List<Skill> Skills = [];
    public List<string> Tags = [];

    public void AddSkill(Skill skill)
    {
        Skills.Add(skill);
    }
}