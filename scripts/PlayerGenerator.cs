namespace DND;

public class PlayerGenerator
{

    public Player Player { get; set; }

    public PlayerGenerator()
    {
        Player = new Player();
        Player.GeneratePlayerStats();
    }
}