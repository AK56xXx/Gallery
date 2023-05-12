using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gallery.Models.Domain
{
    public class Game
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
     
        public string? Image { get; set; }

        public string? Developer { get; set; }
        [Required]

        public string? Publisher { get; set; }
        [Required]
        public string? ReleaseYear { get; set; }

        public virtual ICollection<GamesGenre>? GamesGenres { get; set; } = new List<GamesGenre>();


        [NotMapped]
        [Required]
        public List<int>? Genres { get; set; }

        [NotMapped]
        [Required]
        public IFormFile? ImageFile { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem>? GenreList { get; set; }
        [NotMapped]
        public string? GenreNames { get; set; }
        [NotMapped]
        public MultiSelectList? MultiGenreList { get; set; }

    }
}
