using Godot;

namespace DND.Skills.Warrior;
[GlobalClass]
public partial class HeavyAttack : Skill
{
    protected override void Cast(PlayerData player, EnemyController enemy)
    {
        enemy.TakeDamage(player.Damage * 1.5);
        GD.Print($"Player used skill and deal {player.Damage * 1.5} damage!");
    }
}