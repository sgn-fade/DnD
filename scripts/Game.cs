using Godot;
using System;
using System.Linq;

namespace DND;

public partial class Game : Node
{
    private Scenario _scenario;
    public override void _Ready()
    {
        _scenario = LoadScenarioFromFile("fileExample.json");
        StartGame(_scenario);
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
        return null;
    }

    public void EndGame()
    {

    }
}
