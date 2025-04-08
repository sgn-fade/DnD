using System;
using Godot;

namespace DND;

public class Stat(string type, int value)
{
    public String Type { get; set; } = type;
    public int Value { get; set; } = value;
}