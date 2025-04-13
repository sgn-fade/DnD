using Godot;

namespace DND.Skills.Warrior;

[GlobalClass]
public partial class HeavyAttack : Skill
{
    protected override void Cast(PlayerViewModel player, EnemyController enemy)
    {
        enemy.TakeDamage(player.PlayerData.Damage * 1.5);
        BattleLogText = $"Player used skill and deal {player.PlayerData.Damage * 1.5} damage!";
    }
}