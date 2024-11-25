using System.Collections.Generic;
using Godot;

namespace DND;

public partial class Player : Node
{
    public List<Stat> PlayerStats { get; set; }

    public override void _Ready()
    {
        PlayerStats.Add(new Stat("Dexterity", 0));
        PlayerStats.Add(new Stat("Strength", 0));
        PlayerStats.Add(new Stat("Constitution", 0));
        PlayerStats.Add(new Stat("Intelligence", 0));
    }

    public void GeneratePlayerStats()
    {

    }

    public override string ToString()
    {
        var playerStatsSummary = PlayerStats != null
            ? string.Join("\n", PlayerStats)
            : "None";
        
        return $"Player stats: {playerStatsSummary}\n\r";
    }
}