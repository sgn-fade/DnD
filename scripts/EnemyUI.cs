using Godot;
using System;
using DND;

public partial class EnemyUI : Control
{
	[Export] private Label _nameLabel;
    [Export] private Label _damageLabel;
    [Export] private Label _healthLabel;
    [Export] private TextureRect _enemySprite;


	public void UpdateView(Enemy enemy)
	{
		_nameLabel.Text = enemy.Name;
		_damageLabel.Text = enemy.Damage.ToString();
		_healthLabel.Text = enemy.CurrentHp.ToString();
		_enemySprite.Texture = TextureStorage.Instance.GetEnemyIcon(enemy.Type);
	}
}
