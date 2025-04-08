using System;
using System.Collections.Generic;
using Godot;

namespace DND;
 
public partial class EnemyEncounter : Node
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Outcome OnBattleEnd { get; set; }
    public Outcome OnEnemyKilled { get; set; }
    public List<string> Enemies { get; set; }

    public override string ToString()
    {
        return $"Name: {Name}\n\r" +
               $"Description: {Description}\n\r" +
               $"Outcome:\n\r" +
               $"Battle End: {OnBattleEnd}\n\r" +
               $"Enemy Killed: {OnEnemyKilled}\n\r";
    }
}