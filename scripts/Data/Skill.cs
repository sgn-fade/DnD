using Godot;

namespace DND;

public abstract partial class Skill : Resource
{

    [Export] public string Name { get; set; }
    [Export] public string Description { get; set; }

    [Export] public Texture2D Icon { get; set; }
    [Export] public int Cooldown { get; set; }

    public abstract void Cast(Enemy enemy);
}