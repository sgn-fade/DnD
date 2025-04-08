using System;
using Godot;

namespace DND;

public class Stat(string type, int value)
{
    public string Type { get; set; } = type;
    public int Value { get; set; } = value;
}