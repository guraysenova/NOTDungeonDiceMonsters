public class ServerPlayer
{
    public ServerPlayer(TeamEnum teamEnum, PlayerEnum playerEnum , int id)
    {
        Team = teamEnum;
        Player = playerEnum;
        ID = id;
    }

    public PlayerEnum Player { get; set; }

    public TeamEnum Team { get; set; }

    public int ID { get; set; }
}
