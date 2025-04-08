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
    [Export] private BattleActionsController _actionsController;

    public delegate void OnBattleEnded();
    public delegate void OnPlayerDied();
    public delegate void OnEnemyDied();

    public void StartBattleWith(Enemy enemy)
    {
        _player = PlayerViewModel.Instance;
        _isPlayerTurn = true;
        LoadPlayerSkills();
        var enemyUi = _gameUi.StartBattleMode();
        enemyUi.Enemy = enemy;
        _enemy = enemy;
        _actionsController.CurrentEnemy = enemy;
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

    public void AttackEnemy()
    {
        _enemy.TakeDamage(_player.GetDamage());
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

    public void TryEscapeFromBattle()
    {
        if (PlayerViewModel.Instance.CheckStat(new Stat("dexterity", (int) _enemy.GetEnemyPower())))
        {
            GD.Print("You escaped from battle");
        }
        else
        {
            GD.Print("escape failed!");
        }
        PlayerPressedAction();
    }
}