using Godot;
using System;
using System.Collections.Generic;

public partial class Location : Node
{
    public enum LocationType
    {
        Dungeon,
        DeadForest,
        EndlessBridge,
        CastleRuins,
        FireboundPlato,
    }

    public String Name { get; set; }
    public int Tier { get; set; }
    public String Description { get; set; }
    public List<Event> Events { get; set; }
    public List<EnemyEncounter> EnemyEncounters { get; set; }
    public LocationType Type { get; set; }
}