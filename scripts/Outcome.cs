using Godot;
using System;

public partial class Outcome : Node
{
    public enum OutcomeType
    {
        Fight, 
        Death,
        Reward, 
        NextEvent,
        NextLocation,
    }
    public OutcomeType Type { get; set; }
    public String Body { get; set; }
}
