using Godot;
using System;

public partial class EnemyEncounter : Node
{
    public String Name { get; set; }
    public String Description { get; set; }
    public Enum Type { get; set; }
    public int Hp { get; set; }
    public int Damage { get; set; }
}
