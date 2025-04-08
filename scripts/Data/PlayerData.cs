using System;
using System.Collections.Generic;

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

    public int Hp { get; set; } = 1;
    public int MaxHp { get; set; } = 1;
    public int Damage { get; set; } = 1;
    public int CurrentXp { get; set; }
    public readonly int[] XpThresholds = { 0, 10, 50, 100, 150, 300, 500, 850, 1100, 1500 };
    public int Level { get; set; }

    public int GetPlayerStat(String type)
    {
        return Stats.Find(s => s.Type == type).Value;
    }
}