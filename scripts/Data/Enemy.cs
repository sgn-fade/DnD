using System;
using Godot;

namespace DND;

public partial class Enemy : Node
{
    public String Name { get; set; }
    public String Description { get; set; }
    public string Type { get; set; }
    public Texture2D Sprite { get; set; }

    public double Hp { get; set; }
    public double CurrentHp { get; set; }

    public double Damage { get; set; }

    public double GetEnemyPower() => CurrentHp / 2 + Damage;

    [Signal]
    public delegate void OnEnemyDiedEventHandler();
    public void TakeDamage(int damage)
    {
        CurrentHp -= damage;
        if (CurrentHp <= 0)
        {
            EmitSignalOnEnemyDied();
        }
    }
}