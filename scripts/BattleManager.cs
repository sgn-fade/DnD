using System.Linq;
using Godot;

namespace DND;

public partial class BattleManager : Node2D
{
    [Export] private GameUi _gameUi;
    private bool _isPlayerTurn;
    private PlayerViewModel _player;
    [Export] private Node _battleLog;
    [Export] private EnemyController _enemyController;

    [Signal]
    public delegate void OnBattleEndedEventHandler();
    [Signal]
    public delegate void OnPlayerDiedEventHandler();
    [Signal]
    public delegate void OnEnemyDiedEventHandler();


    public override void _EnterTree()
    {
        _enemyController.OnEnemyDied += PlayerWin;
    }

    public override void _ExitTree()
    {
        _enemyController.OnEnemyDied += PlayerWin;
    }

    public void StartBattleWith(Enemy enemy)
    {
        _player = PlayerViewModel.Instance;
        _isPlayerTurn = true;
        LoadPlayerSkills();
        _gameUi.StartBattleMode();
        _enemyController.Link(enemy);
    }

    private void LoadPlayerSkills()
    {
        //TODO buttons fill
    }

    public void NextTurn()
    {
        if (!_enemyController.IsAlive())
            PlayerWin();

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
        GD.Print("Player Win!!!");
        EmitSignalOnBattleEnded();
        _gameUi.EndBattleMode();
    }

    private void EnemyTurn()
    {
        _isPlayerTurn = true;
        NextTurn();
    }

    public void AttackEnemy()
    {
        GD.Print($"Player deal {_player.GetDamage()} damage! ");
        _enemyController.TakeDamage(_player.GetDamage());
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
        if (PlayerViewModel.Instance.CheckStat(new Stat("dexterity", (int) _enemyController.GetEnemyPower())))
        {
            GD.Print("You escaped from battle");
            EmitSignalOnBattleEnded();
        }
        else
        {
            GD.Print("escape failed!");
        }
        PlayerPressedAction();
    }
}