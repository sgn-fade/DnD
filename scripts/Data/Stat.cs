using System;
using Godot;

namespace DND;

public class Stat
{
    public String Type { get; set; }
    public int Value { get; set; }

    public Stat(string type, int value)
    {
        Type = type;
        Value = value;
    }
    public override string ToString()
    {
        return $"Type: {Type}" +
               $"Value: {Value}\n\r";
    }
}