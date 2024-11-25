using System;
using Godot;

namespace DND;
 
public partial class EnemyEncounter : Node
{
    public String Name { get; set; }
    public String Description { get; set; }
    public Outcome OnBattleEnd { get; set; }
    public Outcome OnEnemyKilled { get; set; }
}