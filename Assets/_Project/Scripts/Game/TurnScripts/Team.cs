using System.Collections.Generic;

public class Team
{
    List<ServerPlayer> players = new List<ServerPlayer>();

    TeamEnum teamEnum;

    public Team(List<ServerPlayer> players, TeamEnum team)
    {
        this.players = players;
        this.teamEnum = team;
    }

    public Team(ServerPlayer player, TeamEnum team)
    {
        this.players = new List<ServerPlayer>() { player };
        this.teamEnum = team;
    }

    public List<ServerPlayer> Players
    {
        get
        {
            return players;
        }
    }
    public TeamEnum TeamEnum
    {
        get
        {
            return teamEnum;
        }
    }
}
