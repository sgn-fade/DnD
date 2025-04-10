using Godot;
using System;
using DND;

public partial class SkillButton : SoundButton
{
    public Skill LinkedSkill { get; set; }

    [Signal]
    public delegate void SkillPressedEventHandler(Skill skill);

    public void OnButtonPressed()
    {
        if (LinkedSkill.IsReady)
            EmitSignalSkillPressed(LinkedSkill);
        else
            GD.Print("Skill is not ready!");
    }

    public void Link(Skill skill)
    {
        LinkedSkill = skill;
        TextureNormal = skill.Icon;
    }
}