using Godot;
using System;

public partial class Weapon : Node
{
    public enum WeaponType
    {
        Sword,
        Daggers,
        Staff,
    }

    public String Name { get; set; }
    public String Description { get; set; }
    public WeaponType Type { get; set; }
    public Stat Stat { get; set; }
}