using Godot;
using System;

public partial class Stat : Node
{
    public enum StatType
    {
        Strength,
        Dexterity,
        Constitution,
        Intelligence,
    }

    public StatType Type { get; set; }
    public int Value { get; set; }
}