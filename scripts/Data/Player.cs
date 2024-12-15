using System.Collections.Generic;
using Godot;

namespace DND;

public partial class Player : Node
{
    public enum PlayerClasses
    {
        Warrior,
        Rogue,
        Wizard,
    }
    public new string Name { get; set; }
    public PlayerClasses Class { get; set; }
    public List<Stat> Stats { get; set; }
    public int Hp { get; set; }
    public int Damage { get; set; }
    public int CurrentXp { get; set; }
    public int MaxXp { get; set; }
    public int Level { get; set; }
    
    public override void _Ready()
    {
        Stats.Add(new Stat("Dexterity", 0));
        Stats.Add(new Stat("Strength", 0));
        Stats.Add(new Stat("Constitution", 0));
        Stats.Add(new Stat("Intelligence", 0));
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
    
    public override string ToString()
    {
        var playerStatsSummary = Stats != null
            ? string.Join("\n", Stats)
            : "None";
        
        return $"Player stats: {playerStatsSummary}\n\r";
    }
}