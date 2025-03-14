using System;
using System.Collections.Generic;

namespace DND;

public partial class PlayerData
{
    public enum PlayerClasses
    {
        Warrior,
        Rogue,
        Mage,
    }
    public string Name { get; set; }
    public PlayerClasses Class { get; set; } = PlayerClasses.Warrior;

    public List<Stat> Stats { get; set; } = new()
    {
        new Stat("strength", 10),
        new Stat("dexterity", 10),
        new Stat("constitution", 10),
        new Stat("intelligence", 10)
    };

    public int Hp { get; set; }
    public int MaxHp { get; set; }
    public int Damage { get; set; }
    public int CurrentXp { get; set; }
    public int MaxXp { get; set; }
    public int Level { get; set; }

    public int GetPlayerStat(String type)
    {
        return Stats.Find(s => s.Type == type).Value;
    }
}