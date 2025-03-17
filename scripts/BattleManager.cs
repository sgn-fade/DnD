using System.Linq;
using Godot;

namespace DND;

public partial class BattleManager : Node2D
{
	[Export] private GameUi _gameGameUi;
	public void StartBattleWith(Enemy enemy)
	{

		var enemyUi = _gameGameUi.StartBattleMode();
		enemyUi.Enemy = enemy;
	}
}
