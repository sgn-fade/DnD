using Godot;

namespace DND;

public abstract partial class Skill : Resource
{
    [Export] public string Name { get; set; }
    [Export] public string Description { get; set; }
    [Export] public Texture2D Icon { get; set; }
    [Export] public PackedScene SceneToSpawn { get; set; }
    [Export] public int Cooldown { get; set; }
    public int CurrentCooldown { get; set; }
    public string BattleLogText { get; set; }
    public bool IsReady => CurrentCooldown <= 0;

    public virtual void Use(PlayerViewModel player, EnemyController enemy)
    {
        CurrentCooldown = Cooldown;
        Cast(player, enemy);
    }
    protected abstract void Cast(PlayerViewModel player, EnemyController enemy);

    public virtual void DecreaseCooldown()
    {
        if (CurrentCooldown > 0)
            CurrentCooldown--;
    }

    public virtual void Reset()
    {
        CurrentCooldown = 0;
    }
}