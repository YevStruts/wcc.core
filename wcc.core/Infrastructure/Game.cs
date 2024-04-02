namespace wcc.core.Infrastructure
{
    public class Game : Document
    {
        public GameType GameType { get; set; }
        public List<string> SideA { get; set; }
        public List<string> SideB { get; set; }
        public int ScoreA { get; set; }
        public int ScoreB { get; set; }
        public string TournamentId { get; set; }
    }
}
