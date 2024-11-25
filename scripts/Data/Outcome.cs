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

    public override string ToString()
    {
        return $"Type: {Type}\n\r" +
               $"Body: {Body}\n\r";
    }
}