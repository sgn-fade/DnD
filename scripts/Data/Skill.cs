using Godot;

namespace DND;

public abstract partial class Skill : Resource
{
    [Export] public string Name { get; set; }
    [Export] public string Description { get; set; }

    [Export] public Texture2D Icon { get; set; }
    [Export] public int Cooldown { get; set; }
    public int CurrentCooldown { get; set; }

    public bool IsReady => CurrentCooldown <= 0;

    public virtual void Use(PlayerData player, EnemyController enemy)
    {
        CurrentCooldown = Cooldown;
        Cast(player, enemy);
    }
    protected abstract void Cast(PlayerData player, EnemyController enemy);

    public virtual void DecreaseCooldown()
    {
        if (CurrentCooldown > 0)
            CurrentCooldown--;
    }
}