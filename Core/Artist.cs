using Core.Interfaces;

namespace Core
{
    public class Artist : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }

        public IList<Album> Albums { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
