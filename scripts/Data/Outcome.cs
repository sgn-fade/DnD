using System;
using Godot;

namespace DND;

public class Outcome
{
    public String Type { get; set; }
    public String Body { get; set; }
    public String tag_to_give {
        get => TagToGive;
        set => TagToGive = value;
    }
    public String TagToGive { get; set; }

    public override string ToString()
    {
        return $"Type: {Type}" +
               $"Body: {Body}\n\r";
    }
}