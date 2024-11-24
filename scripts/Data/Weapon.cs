using System;
using Godot;

namespace DND;

public class Weapon
{
    public String Name { get; set; }
    public String Description { get; set; }
    public Texture2D Sprite { get; set; }
    public Stat Stat { get; set; }
}