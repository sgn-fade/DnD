using System;
using Godot;

namespace DND;

public class Weapon
{
    public String Name { get; set; }
    public String Description { get; set; }
    public Texture2D Sprite { get; set; }
    public Stat Stat { get; set; }

    public override string ToString()
    {
        return $"Name: {Name}\n\r" +
               $"Description: {Description}\n\r" +
               $"Stat: {Stat}\n\r";
    }
}