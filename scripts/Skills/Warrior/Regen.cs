using Godot;

namespace DND.Skills.Warrior;
[GlobalClass]
public partial class Regen : Skill
{
    protected override void Cast(PlayerViewModel player, EnemyController enemy)
    {
        player.HealHp(10);
        GD.Print("Player healed by 10 hp!");
    }
}