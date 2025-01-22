using Godot;

namespace DND;

public partial class TextureStorage : Node2D
{
    //LOCATIONS IMAGES
    [Export] public Texture2D DeadForest { get; set; }
    [Export] public Texture2D Dungeon { get; set; }
    [Export] public Texture2D EndlessBridge { get; set; }
    [Export] public Texture2D CastleRuins { get; set; }
    [Export] public Texture2D FireboundPlato { get; set; }

    //PLAYER ICON CLASSES
    [Export] public Texture2D Warrior { get; set; }
    [Export] public Texture2D Rogue { get; set; }
    [Export] public Texture2D Mage { get; set; }
    public static TextureStorage Instance { get; set; }

    public override void _Ready()
    {
        Instance = this;
    }
}