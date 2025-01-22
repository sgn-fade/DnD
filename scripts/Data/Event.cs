using System;
using System.Collections.Generic;
using Godot;

namespace DND;

public class Event
{
    public String Name { get; set; }
    public String Description { get; set; }
    public List<Action> Actions { get; set; }

    public override string ToString()
    {
        var actionsSummary = Actions != null
            ? string.Join("\n", Actions)
            : "None";
        return $"Name: {Name}\n\r" +
               $"Description: {Description}\n\r" +
               $"Actions: {actionsSummary}\n\r";
    }
}