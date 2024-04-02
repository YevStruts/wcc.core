using wcc.core.Infrastructure;

namespace wcc.core.kernel.Models
{
    public class TournamentModel
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public GameType GameType { get; set; }
        public List<long>? Participants { get; set; }
    }
}
