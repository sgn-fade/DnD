using Godot;

namespace DND;

public class Stat
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