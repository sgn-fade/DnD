using System;
using System.Collections.Generic;
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
}