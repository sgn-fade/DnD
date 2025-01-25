using Godot;
using System;
using DND;

public partial class EnemyUI : Control
{
	private Enemy _enemy;
	//[Export] private Label _nameLabel;
    [Export] private Label _damageLabel;
    [Export] private Label _healthLabel;
    [Export] private TextureRect _enemySprite;
	public Enemy Enemy
	{
		get => _enemy;
		set
		{
			if (value == null) return;
			_enemy = value;
			_damageLabel.Text = value.Damage.ToString();
			_healthLabel.Text = value.Hp.ToString();
			_enemySprite.Texture = GetSpriteByType(_enemy.Type);
		}
	}

	private Texture2D GetSpriteByType(string type)
	{
		return type switch
		{
			"skeleton" => TextureStorage.Instance.Skeleton,
			"slime" => TextureStorage.Instance.Slime,
			"plant" => TextureStorage.Instance.Plant,
			_ => TextureStorage.Instance.Warrior,
		};
	}
}
