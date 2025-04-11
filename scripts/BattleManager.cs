using System;
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
    private int _currentTurn = 1;

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
        _gameUi.PushToBattleLog($"TURN {_currentTurn}\n");
        AllowDoActions();
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
            UpdateSpellsCooldowns();
            AllowDoActions();
            _currentTurn++;
            _gameUi.PushToBattleLog($"TURN {_currentTurn}\n");
        }
        else
        {
            EnemyTurn();
        }

    }

    private void UpdateSpellsCooldowns()
    {
        foreach (var skill in _player.PlayerData.Skills)
        {
            skill.DecreaseCooldown();
        }
    }

    private void PlayerWin()
    {
        GD.Print("Player Win!!!");
        _player.AddXp(_enemyController.GetEnemyPower());
        EndBattle();
    }

    private void EnemyTurn()
    {
        _player.TakeDamage(_enemyController.EnemyData.Damage);
        _gameUi.PushToBattleLog($"Enemy deal {_enemyController.EnemyData.Damage} to player\n");
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
        _gameUi.PushToBattleLog($"Player deal {_player.GetDamage()} damage! \n");
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
            _gameUi.PushToBattleLog("You escaped from battle\n");
            EndBattle();
        }
        else
        {
            _gameUi.PushToBattleLog("escape failed!\n");
            PlayerPressedAction();
        }
    }

    public void UseSkill(Skill skill)
    {
        skill.Use(_player, _enemyController);
        PlayerPressedAction();
    }
}