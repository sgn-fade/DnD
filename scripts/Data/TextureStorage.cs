using System;
using Godot;

namespace DND;

public partial class TextureStorage : Node2D
{
    //LOCATIONS
    [Export] public Texture2D DeadForest { get; set; }
    [Export] public Texture2D Dungeon { get; set; }
    [Export] public Texture2D EndlessBridge { get; set; }
    [Export] public Texture2D CastleRuins { get; set; }
    [Export] public Texture2D FireboundPlato { get; set; }

    //PLAYER CLASS ICONS
    [Export] public Texture2D Warrior { get; set; }
    [Export] public Texture2D Rogue { get; set; }
    [Export] public Texture2D Mage { get; set; }

    //ENEMIES
    [Export] public Texture2D Skeleton { get; set; }
    [Export] public Texture2D Slime { get; set; }
    [Export] public Texture2D Plant { get; set; }

    public static TextureStorage Instance { get; set; }

    public override void _Ready()
    {
        Instance = this;
    }

    public Texture2D GetPlayerIcon(PlayerData.PlayerClasses @class)
    {
        return @class switch
        {
            PlayerData.PlayerClasses.Warrior => Warrior,
            PlayerData.PlayerClasses.Rogue => Rogue,
            PlayerData.PlayerClasses.Mage => Mage,
            _ => throw new ArgumentOutOfRangeException(nameof(@class), @class, null)
        };
    }
    public Texture2D GetEnemyIcon(string type)
    {
        return type switch
        {
            "skeleton" => Skeleton,
            "slime" => Slime,
            "plant" => Plant,
            _ => Warrior,
        };
    }
    public Texture2D GetLocationBackground(string type)
    {
        return type switch
        {
            "dungeon" => Dungeon,
            "dead_forest" => DeadForest,
            "endless_bridge" => EndlessBridge,
            "castle_ruins" =>CastleRuins,
            "firebound_plato" => FireboundPlato,
            _ => Dungeon
        };
    }
}