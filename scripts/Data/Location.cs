using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace DND;

public class Location
{
    public string Name { get; set; }
    public int Tier { get; set; }
    public string Type { get; set; }
    public String Description { get; set; }
    public List<Event> Events { get; set; }
    public List<EnemyEncounter> EnemyEncounters { get; set; }
    public List<EnemyEncounter> enemy_encounters
    {
        get => EnemyEncounters;
        set => EnemyEncounters = value;
    }
}