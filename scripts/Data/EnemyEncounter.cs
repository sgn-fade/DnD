using System;
using Godot;

namespace DND;
 
public partial class EnemyEncounter : Node
{
    public String Name { get; set; }
    public String Description { get; set; }
    public Outcome OnBattleEnd { get; set; }
    public Outcome OnEnemyKilled { get; set; }

    public override string ToString()
    {
        return $"Name: {Name}\n\r" +
               $"Description: {Description}\n\r" +
               $"Outcome:\n\r" +
               $"Battle End: {OnBattleEnd}\n\r" +
               $"Enemy Killed: {OnEnemyKilled}\n\r";
    }
}