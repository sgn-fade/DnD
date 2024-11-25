using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace DND;

public partial class Location : Node
{
    public String Name { get; set; }
    public int Tier { get; set; }
    public String Description { get; set; }
    public List<Event> Events { get; set; }
    public List<EnemyEncounter> EnemyEncounters { get; set; }
    public Texture2D Background { get; set; }
    public override string ToString()
    {
        var eventsSummary = Events != null
            ? string.Join(", ", Events.Select(e => e.Name))
            : "None";

        var encountersSummary = EnemyEncounters != null
            ? string.Join(", ", EnemyEncounters.Select(e => e.Name))
            : "None";

        return $"Name: {Name}, " +
               $"Tier: {Tier}, " +
               $"Description: {Description}, " +
               $"\n\rEvents: [{eventsSummary}], " +
               $"\n\rEnemy Encounters: [{encountersSummary}]";
    }
}