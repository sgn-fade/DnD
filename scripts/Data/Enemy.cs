using System;
using Godot;

namespace DND;

public partial class Enemy : Node
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public Texture2D Sprite { get; set; }

    public double Hp { get; set; }
    public double CurrentHp { get; set; }

    public double Damage { get; set; }

}