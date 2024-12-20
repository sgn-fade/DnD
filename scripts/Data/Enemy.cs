using System;
using Godot;

namespace DND;

public partial class Enemy : Node
{
    public String Name { get; set; }
    public String Description { get; set; }
    public string Type { get; set; }
    public Texture2D Sprite { get; set; }
    public int Hp { get; set; }
    public int Damage { get; set; }

    public override string ToString()
    {
        return $"Name: {Name}\n\r" +
               $"Description: {Description}\n\r" +
               $"Type: {Type}\n\r" +
               $"Hp: {Hp}\n\r" +
               $"Damage: {Damage}\n\r";
    }
}