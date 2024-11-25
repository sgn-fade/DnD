using System.Collections.Generic;
using Godot;

namespace DND;

public partial class Player : Node
{
    public List<Stat> PlayerStats { get; set; }

    public override void _Ready()
    {
        PlayerStats.Add(new Stat());
    }

    public void GeneratePlayerStats()
    {

    }
}