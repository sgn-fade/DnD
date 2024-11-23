using Godot;
using System;
using System.Collections.Generic;

public partial class Event : Node
{
    public String Name { get; set; }
    public String Description { get; set; }
    public List<Action> Actions { get; set; }
}
