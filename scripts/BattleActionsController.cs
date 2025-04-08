using Godot;

namespace DND;

public partial class BattleActionsController : Control
{
    public Enemy CurrentEnemy { get; set; }
    [Export] public BattleManager BattleManager { get; set; }
    [Export] public Control SkillsMenu { get; set; }
    [Export] public Control InventoryMenu { get; set; }

    public void OnAttackPressed()
    {
        BattleManager.AttackEnemy();
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
    }
}