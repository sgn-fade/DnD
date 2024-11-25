using System;
using Godot;

namespace DND;

public class Outcome
{
    public String Type { get; set; }
    public String Body { get; set; }

    public override string ToString()
    {
        return $"Type: {Type}" +
               $"Body: {Body}\n\r";
    }
}