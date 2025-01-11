namespace Core
{
    public class Artist
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }

        public IList<Album> Albums { get; set; }
    }
}
