using Godot;
using System;
using System.Linq;
using System.IO;
using Newtonsoft.Json;

namespace DND;

public partial class Game : Node
{
    private Scenario _scenario;
    private Location _currentLocation;
    [Export] private BattleManager _battleManager;
    [Export] private GameUi _gameUi;

    public override void _Ready()
    {
        PlayerViewModel.Instance.Init(new PlayerData());
        _scenario = LoadScenarioFromFile("scripts/the_long_way.json");
        StartGame(_scenario);
        ActionButtons.OnActionPressed += OnActionButtonPressed;
    }

    public void StartGame(Scenario scenario)
    {
        ProcessLocation(scenario.Locations.First());
    }

    public void ProcessLocation(Location location)
    {
        _currentLocation = location;
        _gameUi.ChangeLocation(location);
        EventProcess(location.Events.First());
    }

    public void EventProcess(Event @event)
    {
        _gameUi.ChangeEvent(@event);
    }

    public async void OnActionButtonPressed(Action action)
    {
        Outcome outcome;
        if (!PlayerViewModel.Instance.CheckStat(action.RequiredStat) && action.RequiredStat != null)
        {
            var diceRoller = DiceRoller.Instance;
            diceRoller.RollDice();
            Variant[] result = await ToSignal(diceRoller, "DiceRolled");
            int diceValue = (int)result[0];

            outcome = PlayerViewModel.Instance.CheckStat(action.RequiredStat, diceValue)
                ? action.PositiveOutcome
                : action.NegativeOutcome;
        }
        else
        {
            outcome = action.PositiveOutcome;
        }

        ResolveOutcome(outcome);
    }

    private void ResolveOutcome(Outcome outcome)
    {
        if (outcome.TagToGive != null) PlayerViewModel.Instance.AddTag(outcome.TagToGive);
        switch (outcome.Type)
        {
            case "next_event":
                var @event = _currentLocation.Events.FirstOrDefault(e => e.Name == outcome.Body);
                if (@event == null)
                {
                    var encounter = _currentLocation.EnemyEncounters.FirstOrDefault(e => e.Name == outcome.Body);

                    var enemyName = encounter?.Enemies.FirstOrDefault();
                    var enemy = _scenario.GetEnemyByName(enemyName);
                    _battleManager.StartBattleWith(enemy);
                }
                else
                    EventProcess(@event);

                break;
            case "change_location":
                ProcessLocation(_scenario.Locations.First(l => l.Name == outcome.Body));
                break;
            case "death":
                EndGame();
                break;
        }
    }

    public Scenario LoadScenarioFromFile(string filename)
    {
        var jsonString = File.ReadAllText(filename);
        return JsonConvert.DeserializeObject<Scenario>(jsonString);
    }

    public void EndGame()
    {
        GD.PrintRich("[color=red]YOU DIED!![/color]");
        GetTree().Quit();
    }
}