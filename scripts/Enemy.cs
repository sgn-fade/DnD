using Godot;
using System;

public partial class Enemy : Node
{
    public enum EnemyType
    {
        Goblin,
        Skeleton,
        Mimic,
        Fiend,
        Dragon,
    }

    public String Name { get; set; }
    public String Description { get; set; }
    public EnemyType Type { get; set; }
    public int Hp { get; set; }
    public int Damage { get; set; }
}