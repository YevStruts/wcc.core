namespace wcc.core.Infrastructure
{
    public class Game
    {
        public long GameId { get; set; }
        public GameType GameType { get; set; }
        public List<long> SideA { get; set; }
        public List<long> SideB { get; set; }
        public int ScoreA { get; set; }
        public int ScoreB { get; set; }
        public long TournamentId { get; set; }
    }
}
