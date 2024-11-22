using System;
using System.Collections.Generic;
using Godot;

namespace DND;

public partial class Action : Node
{
    public String Description { get; set; }
    public Stat RequiredStat { get; set; }
    public Outcome PositiveOutcome { get; set; }
    public Outcome NegativeOutcome { get; set; }
    public List<int> ItemsToGive { get; set; }
}