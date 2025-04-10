using Godot;
using System;
using DND;

public partial class SkillButton : SoundButton
{

    public Skill LinkedSkill { get; set; }

    [Signal]
    public delegate void SkillPressedEventHandler(Skill skill);

    public override void _Ready()
    {

    }

    public void Link(Skill skill)
    {
        LinkedSkill = skill;
        TextureNormal = skill.Icon;
    }
}
