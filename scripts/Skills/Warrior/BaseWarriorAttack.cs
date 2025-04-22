using Godot;

namespace DND.Skills.Warrior;
[GlobalClass]
public partial class BaseWarriorAttack : Skill
{
    protected override void Cast(PlayerViewModel player, EnemyController enemy)
    {
        enemy.TakeDamage(player.GetDamage());
        BattleLogText = $"Player deal {player.GetDamage()} damage!";
    }
}