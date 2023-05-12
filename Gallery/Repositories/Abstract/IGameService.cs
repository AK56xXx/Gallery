using Gallery.Models.Domain;
using Gallery.Models.DTO;

namespace Gallery.Repositories.Abstract
{
    public interface IGameService
    {
        bool Add(Game model);
        bool Update(Game model);

        Game GetById(int id);

        bool Delete(int id);

        GameListVm List(string term = "", bool paging = false, int currentPage = 0);
        List<int> GetGenreByGameId(int gameId);
    }
}
