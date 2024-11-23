using Godot;
using System;
using System.Collections.Generic;

public partial class Scenario : Node
{
    public String Name { get; set; }
    public List<Location> Locations { get; set; }
    public List<Weapon> WeaponPool { get; set; }
    public List<Enemy> EnemyPool { get; set; }
}