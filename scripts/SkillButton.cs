using Godot;
using System;
using DND;

public partial class SkillButton : SoundButton
{
    public Skill LinkedSkill { get; set; }

    [Signal]
    public delegate void SkillPressedEventHandler(Skill skill);

    [Export] private DescriptionPopUp _descriptionPopUp;
    [Export] protected AudioStreamPlayer _skillNotReadyAudioPlayer;

    [Export] private TextureRect _skillIcon;
    [Export] private ColorRect _cdBack;
    [Export] private Label _cdText;

    public override void _Ready()
    {
        base._Ready();
        Disabled = true;
    }

    protected override void OnPressed()
    {
        if (LinkedSkill.IsReady)
        {
            EmitSignalSkillPressed(LinkedSkill);
            base.OnPressed();
        }
        else
        {
            GD.Print("Skill is not ready!");
            _skillNotReadyAudioPlayer.Play();
        }
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
        if (LinkedSkill != null)
        {
            _descriptionPopUp.Show();
            _descriptionPopUp.DisplayText(LinkedSkill.Name, LinkedSkill.Description);
        }
    }

    public void OnMouseExited()
    {
        _descriptionPopUp.Hide();
    }

    public void UpdateView()
    {
        _cdText.Visible = _cdBack.Visible = !LinkedSkill.IsReady;
        _cdText.Text = $"{LinkedSkill.CurrentCooldown}";
    }
}