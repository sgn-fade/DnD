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
    public override void _Ready()
    {
        _scenario = LoadScenarioFromFile("scripts/the_long_way.json");
        Console.WriteLine(_scenario);
        StartGame(_scenario);
    }

    public void StartGame(Scenario scenario)
    {
        scenario.Locations.ForEach(LoopLocation);
    }

    public void LoopLocation(Location location)
    {
        _gameUI.ChangeLocation(location);
        location.Events.ForEach(EventProcess);
    }

    public void EventProcess(Event @event)
    {
        _gameUI.ChangeEvent(@event);
    }
    
    public void CreateActionButtons(Event @event)
    {

    }

    public void OnActionButtonPressed(Action action)
    {

    }
    public Scenario LoadScenarioFromFile(string filename)
    {
        var jsonString = File.ReadAllText(filename);
        return JsonConvert.DeserializeObject<Scenario>(jsonString);
    }

    public void EndGame()
    {

    }
}
