using Microsoft.Build.Framework;

namespace Gallery.Models.Domain
{
    public class Genre
    {
        public int Id { get; set; }
        [Required]
        public string? GenreName { get; set; }

        public virtual ICollection<GamesGenre>? GamesGenres { get; set; } = new List<GamesGenre>();
    }
}
