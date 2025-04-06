using System.Linq;
using Godot;

namespace DND;

public partial class BattleManager : Node2D
{
    [Export] private GameUi _gameUi;
    private bool _isPlayerTurn;
    private PlayerViewModel _player;
    private Node _battleLog;
    private Enemy _enemy;

    public void StartBattleWith(Enemy enemy)
    {
        _player = PlayerViewModel.Instance;
        _isPlayerTurn = true;
        LoadPlayerSkills();
        var enemyUi = _gameUi.StartBattleMode();
        enemyUi.Enemy = enemy;
        _enemy = enemy;
    }

    private void LoadPlayerSkills()
    {
        //TODO buttons fill
    }

    public void NextTurn()
    {
        if (_enemy.Hp <= 0)
        {
            PlayerWin();
        }
        if (_isPlayerTurn)
        {
            AllowDoActions();
        }
        else
        {
            EnemyTurn();
        }
    }

    private void PlayerWin()
    {
        //TODO end battle by player win
    }

    private void EnemyTurn()
    {
        _isPlayerTurn = true;
        NextTurn();
    }

    private void AllowDoActions()
    {
//TODO on player actions
    }

    private void PlayerPressedAction()
    {
        _isPlayerTurn = false;
        NextTurn();
        //TODO off player actions
    }

    private async void TryEscapeFromBattle()
    {
        var diceRoller = DiceRoller.Instance;
        diceRoller.RollDice();
        Variant[] result = await ToSignal(diceRoller, "DiceRolled");

        //TODO check result with enemy power*
    }
}