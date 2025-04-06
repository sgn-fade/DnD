using Godot;
using System;
using DND;

public partial class SkillButton : TextureButton
{

    public Node LinkedSkill { get; set; }

    [Signal]
    public delegate void SkillPressedEventHandler(Skill skill);

    public override void _Ready()
    {

    }
}
