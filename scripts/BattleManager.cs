using System.Linq;
using Godot;

namespace DND;

public partial class BattleManager : Node2D
{
	[Export] private GameUi _gameUi;
	public void StartBattleWith(Enemy enemy)
	{

		var enemyUi = _gameUi.StartBattleMode();
		enemyUi.Enemy = enemy;
	}
}
