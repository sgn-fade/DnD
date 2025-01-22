using Godot;

namespace DND;

public partial class TextureStorage : Node2D
{
    [Export] public Texture2D DeadForest { get; set; }
    [Export] public Texture2D Dungeon { get; set; }
    [Export] public Texture2D EndlessBridge { get; set; }
    [Export] public Texture2D CastleRuins { get; set; }
    [Export] public Texture2D FireboundPlato { get; set; }
    public static TextureStorage Instance { get; set; }

    public override void _Ready()
    {
        Instance = this;
    }
}