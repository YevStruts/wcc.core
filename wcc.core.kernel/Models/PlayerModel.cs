using wcc.core.Infrastructure;

namespace wcc.core.kernel.Models
{
    public class PlayerModel
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public bool IsActive { get; set; }
        public string? UserId { get; set; }
        public string? Token { get; set; }
    }
}
