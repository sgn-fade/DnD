using System.Linq;
using Godot;

namespace DND;

public partial class BattleManager : Node2D
{
	[Export] private UI _gameUI;
	public void StartBattleWith(Enemy enemy)
	{
		var enemyUi = _gameUI.StartBattleMode();
		enemyUi.Enemy = enemy;
	}

}
