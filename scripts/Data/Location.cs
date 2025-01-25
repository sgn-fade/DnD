using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace DND;

public class Location
{
    public new string Name { get; set; }
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

    public override string ToString()
    {
        var eventsSummary = Events != null
            ? string.Join("\n", Events)
            : "None";

        var encountersSummary = EnemyEncounters != null
            ? string.Join("\n", EnemyEncounters)
            : "None";

        return $"Name: {Name}\n\r" +
               $"Tier: {Tier}\n\r" +
               $"Type: {Type}\n\r" +
               $"Description: {Description}\n\r" +
               $"Events: [{eventsSummary}]\n\r" +
               $"Enemy Encounters: [{encountersSummary}]\n\r";
    }
}