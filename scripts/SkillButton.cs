using Godot;
using System;
using DND;

public partial class SkillButton : SoundButton
{
    public Skill LinkedSkill { get; set; }

    [Signal]
    public delegate void SkillPressedEventHandler(Skill skill);

    [Export] private DescriptionPopUp _descriptionPopUp;
    [Export] private TextureRect _skillIcon;

    public override void _Ready()
    {
        base._Ready();
        Disabled = true;
    }

    public void OnButtonPressed()
    {
        if (LinkedSkill.IsReady)
            EmitSignalSkillPressed(LinkedSkill);
        else
            GD.Print("Skill is not ready!");
    }

    public void Link(Skill skill)
    {
        Disabled = false;
        Show();
        LinkedSkill = skill;
        _skillIcon.Texture = skill.Icon;
    }

    public void OnMouseEntered()
    {
        if(LinkedSkill != null)
        {
            _descriptionPopUp.Show();
            _descriptionPopUp.DisplayText(LinkedSkill.Name, LinkedSkill.Description);
        }
    }
    public void OnMouseExited()
    {
        _descriptionPopUp.Hide();
    }
}