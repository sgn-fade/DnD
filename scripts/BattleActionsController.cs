using System.Collections.Generic;
using Godot;

namespace DND;

public partial class BattleActionsController : Control
{
    [Export] public BattleManager BattleManager { get; set; }
    [Export] public Control SkillsMenu { get; set; }

    private List<SkillButton> _skillButtons = [];


    public override void _Ready()
    {
        foreach (var node in SkillsMenu.GetChildren())
        {
            if (node is SkillButton skillButton)
            {
                _skillButtons.Add(skillButton);
                skillButton.SkillPressed += OnSkillPressed;
            }
        }
    }

    public void OnAttackPressed()
    {
        BattleManager.AttackEnemy();
        DisableButtons();
    }

    public void InitSkillButtons(PlayerData player)
    {
        for (int i = 0; i < player.Skills.Count; i++)
        {
            _skillButtons[i].Link(player.Skills[i]);
        }
    }

    private void OnSkillPressed(Skill skill)
    {
        BattleManager.UseSkill(skill);
    }

    public override void _ExitTree()
    {
        foreach (var button in _skillButtons)
        {
            button.SkillPressed -= OnSkillPressed;
        }
    }


    public void OnEscapePressed()
    {
        BattleManager.TryEscapeFromBattle();
        DisableButtons();
    }

    public void DisableButtons()
    {
        foreach (var textureButton in _skillButtons)
        {
            textureButton.Disabled = true;
        }
    }

    public void EnableButtons()
    {
        foreach (var textureButton in _skillButtons)
        {
            textureButton.Disabled = false;
        }
    }

    public void UpdateSkillButtonsView()
    {
        foreach (var skillButton in _skillButtons)
        {
            if(skillButton.LinkedSkill != null)
                skillButton.UpdateView();
        }
    }
}