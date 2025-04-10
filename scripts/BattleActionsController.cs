using System.Collections.Generic;
using Godot;

namespace DND;

public partial class BattleActionsController : Control
{
    [Export] public BattleManager BattleManager { get; set; }
    [Export] public Control SkillsMenu { get; set; }
    [Export] public Control InventoryMenu { get; set; }

    [Export] public TextureButton[] _buttonsToDisable;
    private List<SkillButton> _skillButtons = [];


    public override void _Ready()
    {
        foreach (var node in SkillsMenu.GetChildren())
        {
            if (node is SkillButton skillButton) _skillButtons.Add(skillButton);
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
            _skillButtons[i].SkillPressed += OnSkillPressed;
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

    public void OnSkillsPressed() => ToggleMenu(SkillsMenu);
    public void OnInventoryPressed() => ToggleMenu(InventoryMenu);

    private void ToggleMenu(Control menu)
    {
        menu.Visible = !menu.Visible;
    }

    private void HideAllMenus()
    {
        SkillsMenu.Visible = false;
        InventoryMenu.Visible = false;
    }

    public void OnEscapePressed()
    {
        BattleManager.TryEscapeFromBattle();
        DisableButtons();
    }

    public void DisableButtons()
    {
        foreach (var textureButton in _buttonsToDisable)
        {
            textureButton.Disabled = true;
        }
        HideAllMenus();
    }
    public void EnableButtons()
    {
        foreach (var textureButton in _buttonsToDisable)
        {
            textureButton.Disabled = false;
        }
    }
}