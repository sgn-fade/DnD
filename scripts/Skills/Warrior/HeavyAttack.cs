using System;
using Godot;

namespace DND.Skills.Warrior;

[GlobalClass]
public partial class HeavyAttack : Skill
{
    protected override void Cast(PlayerViewModel player, EnemyController enemy)
    {
        var damage = Math.Round(player.PlayerData.Damage * 1.5, 1);
        enemy.TakeDamage(damage);
        BattleLogText = $"Player used skill and deal {damage} damage!";
    }
}