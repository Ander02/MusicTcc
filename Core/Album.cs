namespace Core
{
    public class Album
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public DateTime ReleasedAt { get; set; }
        public string Base64Image { get; set; }

        public IList<Artist> Artists { get; set; }
        public IList<Music> Musics { get; set; }
    }
}
