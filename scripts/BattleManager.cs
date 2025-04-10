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
    [Export] private BattleActionsController _battleActionsController;

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
        _enemyController.OnEnemyDied -= PlayerWin;
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
        _battleActionsController.InitSkillButtons(_player.PlayerData);
    }

    public void NextTurn()
    {
        if (!_enemyController.IsAlive())
            PlayerWin();

        _isPlayerTurn = !_isPlayerTurn;

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
        EndBattle();
    }

    private void EnemyTurn()
    {
        _player.TakeDamage(_enemyController.EnemyData.Damage);
        GD.Print($"Enemy deal {_enemyController.EnemyData.Damage} to player");
        NextTurn();
    }

    private void EndBattle()
    {
        EmitSignalOnBattleEnded();
        _gameUi.EndBattleMode();
        _enemyController.Hide();
    }
    public void AttackEnemy()
    {
        GD.Print($"Player deal {_player.GetDamage()} damage! ");
        _enemyController.TakeDamage(_player.GetDamage());
        PlayerPressedAction();
    }
    private void AllowDoActions()
    {
        _battleActionsController.EnableButtons();
    }

    private void PlayerPressedAction()
    {
        _battleActionsController.DisableButtons();
        NextTurn();
    }

    public void TryEscapeFromBattle()
    {
        if (PlayerViewModel.Instance.CheckStat(new Stat("dexterity", (int) _enemyController.GetEnemyPower())))
        {
            GD.Print("You escaped from battle");
            EndBattle();
        }
        else
        {
            GD.Print("escape failed!");
            PlayerPressedAction();
        }
    }
}