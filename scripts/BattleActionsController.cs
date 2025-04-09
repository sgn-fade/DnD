using System.Collections.Generic;
using Godot;

namespace DND;

public partial class BattleActionsController : Control
{
    [Export] public BattleManager BattleManager { get; set; }
    [Export] public Control SkillsMenu { get; set; }
    [Export] public Control InventoryMenu { get; set; }

    [Export] public TextureButton[] _buttonsToDisable;

    public void OnAttackPressed()
    {
        BattleManager.AttackEnemy();
        DisableButtons();
    }

    public void OnSkillsPressed() => ToggleMenu(SkillsMenu);
    public void OnInventoryPressed() => ToggleMenu(InventoryMenu);

    private void ToggleMenu(Control menu)
    {
        HideAllMenus();
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
    }
    public void EnableButtons()
    {
        foreach (var textureButton in _buttonsToDisable)
        {
            textureButton.Disabled = false;
        }
    }
}