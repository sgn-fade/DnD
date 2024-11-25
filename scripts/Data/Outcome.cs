using System;
using Godot;

namespace DND;

public class Outcome
{
    public enum OutcomeType
    {
        Death,
        NextEvent,
        NextLocation,
    }
    public OutcomeType Type { get; set; }
    public String Body { get; set; }
}