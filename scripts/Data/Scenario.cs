using System;
using System.Collections.Generic;
using DND;
using Godot;

namespace DND;

public partial class Scenario : Node
{
    public String Name { get; set; }
    public List<Location> Locations { get; set; }
    public List<Weapon> WeaponPool { get; set; }
    public List<Enemy> EnemyPool { get; set; }

    public Enemy GetRandomEnemy() => EnemyPool[(int)(GD.Randi() % EnemyPool.Count)];
    public Weapon GetRandomWeapon() => WeaponPool[(int)(GD.Randi() % WeaponPool.Count)];
    public Enemy GetEnemyByName(String name) => EnemyPool.Find(enemy => enemy.Name == name);
    public Weapon GetWeaponByName(String name) => WeaponPool.Find(weapon => weapon.Name == name);
}