namespace Gallery.Models.Domain
{
    public class GamesGenre
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int GenreId { get; set; }

        public virtual Game Game { get; set; } = null!;
        public virtual Genre Genre { get; set; } = null!;
    }
}
