using System;
using Godot;

namespace DND;

public class Weapon
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