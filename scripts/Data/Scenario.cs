using System;
using System.Collections.Generic;
using DND;
using Godot;

namespace DND;

public partial class Scenario : Node
{
    public String Name { get; set; }
    public List<Location> Locations { get; set; }
    public List<Weapon> Weapons { get; set; }
    public List<Enemy> Enemies { get; set; }

    public Enemy GetRandomEnemy() => Enemies[(int)(GD.Randi() % Enemies.Count)];
    public Weapon GetRandomWeapon() => Weapons[(int)(GD.Randi() % Weapons.Count)];
    public Enemy GetEnemyByName(String name) => Enemies.Find(enemy => enemy.Name == name);
    public Weapon GetWeaponByName(String name) => Weapons.Find(weapon => weapon.Name == name);

    public override string ToString()
    {
        var locationsSummary = Locations != null
            ? string.Join("\n", Locations)
            : "None";
        var weaponsSummary = Weapons!= null
            ? string.Join("\n", Weapons)
             : "None";
        var enemiesSummary = Enemies!= null
            ? string.Join("\n", Enemies)
             : "None";

        return $"Scenario Name: {Name}\n\r" +
               $"Locations:{locationsSummary}\n\r" +
               $"Weapons:{weaponsSummary}\n\r" +
               $"Enemies:{enemiesSummary}";

    }
}