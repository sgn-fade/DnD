using System;
using System.Collections.Generic;
using Godot;

namespace DND;

public partial class Action : Node
{
    public String Description { get; set; }
    public Stat required_stat
    {
        get => RequiredStat;
        set => RequiredStat = value;
    }
    public Stat RequiredStat { get; set; }

    public Outcome positive_outcome
    {
        get => PositiveOutcome;
        set => PositiveOutcome = value;
    }

    public Outcome negative_outcome {
        get => NegativeOutcome;
        set => NegativeOutcome = value;
    }
    public Outcome PositiveOutcome { get; set; }
    public Outcome NegativeOutcome { get; set; }

    public override string ToString()
    {
        return $"Description: {Description}\n\r" +
               $"Required stat: {RequiredStat}\n\r" +
               $"Outcome:\n" +
               $"Positive: {PositiveOutcome}\n" +
               $"Negative: {NegativeOutcome}\n\r";
    }
}