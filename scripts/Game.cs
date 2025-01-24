using Godot;
using System;
using System.Linq;
using System.IO;
using Newtonsoft.Json;

namespace DND;

public partial class Game : Node
{
    private Scenario _scenario;
    [Export] private UI _gameUI;
    private Location _currentLocation;

    public override void _Ready()
    {
        _scenario = LoadScenarioFromFile("scripts/the_long_way.json");
        Console.WriteLine(_scenario);
        StartGame(_scenario);

        ActionButtons.OnActionPressed += OnActionButtonPressed;
    }

    public void StartGame(Scenario scenario)
    {
        scenario.Locations.ForEach(LoopLocation);
        Player.Instance.Start();
    }

    public void LoopLocation(Location location)
    {
        _currentLocation = location;
        _gameUI.ChangeLocation(location);
        EventProcess(location.Events.First());
    }

    public void EventProcess(Event @event)
    {
        _gameUI.ChangeEvent(@event);
    }

    public void OnActionButtonPressed(Action action)
    {
        var outcome = (action.RequiredStat == null || CheckPlayerStat(action.RequiredStat))
            ? action.PositiveOutcome
            : action.NegativeOutcome;

        switch (outcome.Type)
        {
            case "next_event":
                EventProcess(_currentLocation.Events.First(e => e.Name == outcome.Body));
                break;
            case "change_location":
                LoopLocation(_scenario.Locations.First(l => l.Name == outcome.Body));
                break;
            case "death":
                EndGame();
                break;
            default: return;
        }
    }

    public bool CheckPlayerStat(Stat stat)
    {
        if (stat.Type == null) return true;
        return stat.Value <= Player.Instance.GetPlayerStat(stat.Type);
    }
    public Scenario LoadScenarioFromFile(string filename)
    {
        var jsonString = File.ReadAllText(filename);
        return JsonConvert.DeserializeObject<Scenario>(jsonString);
    }

    public void EndGame()
    {
        GetTree().Quit();
    }
}