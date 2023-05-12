using Gallery.Models.Domain;

namespace Gallery.Models.DTO
{
    public class GameListVm
    {
        public IQueryable<Game> GameList { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string? Term { get; set; }
    }
}
