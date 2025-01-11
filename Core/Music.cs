using Core.Interfaces;

namespace Core
{
    public class Music : IEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public IList<Album> Albums { get; set; }
        public IList<Genre> Genres { get; set; }
        public IList<Rating> Ratings { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
