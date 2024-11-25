using Godot;
using System;
using System.Linq;
using System.IO;
using Newtonsoft.Json;

namespace DND;

public partial class Game : Node
{
    private Scenario _scenario;
    public override void _Ready()
    {
        _scenario = LoadScenarioFromFile("scripts/the_long_way.json");
        Console.WriteLine(_scenario);
        _scenario = null;
    }

    public void StartGame(Scenario scenario)
    {
        scenario.Locations.ForEach(LoopLocation);
    }

    public void LoopLocation(Location location)
    {

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
