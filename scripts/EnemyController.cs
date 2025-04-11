using Godot;

namespace DND;

public partial class EnemyController : Node
{
	public Enemy EnemyData { get; set; }

	[Export] public EnemyUI EnemyView { get; set; }

	public double GetEnemyPower() => EnemyData.CurrentHp + EnemyData.Damage * 10;

	[Signal]
	public delegate void OnEnemyDiedEventHandler();
	public void TakeDamage(double damage)
	{
		EnemyData.CurrentHp -= damage;
		EnemyView.UpdateView(EnemyData);
		if (EnemyData.CurrentHp <= 0)
		{
			EmitSignalOnEnemyDied();
			Hide();
		}
	}

	public override void _Ready()
	{
		Hide();
	}

	public void Link(Enemy enemy)
	{
		EnemyData = enemy;
		EnemyData.CurrentHp = EnemyData.Hp;
		EnemyView.UpdateView(EnemyData);
		EnemyView.Visible = true;

	}

	public void Hide()
	{
		EnemyView.Visible = false;
	}
	public bool IsAlive() => EnemyData.Hp > 0;
}
