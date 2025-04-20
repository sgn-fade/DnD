using Godot;

namespace DND.Skills.Warrior;
[GlobalClass]
public partial class BloodyExchange : Skill
{
    protected override void Cast(PlayerViewModel player, EnemyController enemy)
    {
        player.TakeDamage(10);
        player.ResetAllSkills();
    }
}