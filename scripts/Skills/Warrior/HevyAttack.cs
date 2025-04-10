using Godot;

namespace DND.Skills.Warrior;
[GlobalClass]
public partial class HevyAttack : Skill
{
    protected override void Cast(PlayerData player, EnemyController enemy)
    {
        enemy.TakeDamage(player.Damage * 1.5);
    }
}