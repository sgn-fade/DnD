using System;
using Godot;

namespace DND;

public partial class Enemy : Node
{
    public String Name { get; set; }
    public String Description { get; set; }
    public Texture2D Sprite { get; set; }
    public int Hp { get; set; }
    public int Damage { get; set; }
}