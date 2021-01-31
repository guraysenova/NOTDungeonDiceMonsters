class Turn
{
    public int TeamIndex { get; set; }
    public int PlayerIndexInTeam { get; set; }
    public TurnPhase Phase { get; set; }
    public Turn()
    {
        TeamIndex = 0;
        PlayerIndexInTeam = 0;
        Phase = TurnPhase.Roll;
    }
}