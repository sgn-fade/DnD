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


    public List<Stat> Stats { get; set; } =
    [
        new("strength", 10),
        new("dexterity", 10),
        new("constitution", 10),
        new("intelligence", 10)
    ];

    public double Hp { get; set; } = 100;
    public double MaxHp { get; set; } = 100;
    public double Damage { get; set; } = 1;
    public double CurrentXp { get; set; }
    public readonly int[] XpThresholds = { 0, 10, 50, 100, 150, 300, 500, 850, 1100, 1500 };
    public int Level { get; set; }

    public int GetPlayerStat(String type)
    {
        return Stats.Find(s => s.Type == type).Value;
    }
}