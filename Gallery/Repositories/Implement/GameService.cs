using Gallery.Models.Domain;
using Gallery.Models.DTO;
using Gallery.Repositories.Abstract;

namespace Gallery.Repositories.Implement
{
    public class GameService : IGameService
    {
        private readonly DatabaseContext ctx;
        public GameService(DatabaseContext ctx)
        {
            this.ctx = ctx;
        }
        public bool Add(Game model)
        {
            try
            {

                ctx.Game.Add(model);
                ctx.SaveChanges();
                foreach (int genreId in model.Genres)
                {
                    var gameGenre = new GamesGenre
                    {
                        GameId = model.Id,
                        GenreId = genreId
                    };
                    ctx.GamesGenre.Add(gameGenre);
                }
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var data = this.GetById(id);
                if (data == null)
                    return false;
                var gameGenres = ctx.GamesGenre.Where(a => a.GameId == data.Id);
                foreach (var gameGenre in gameGenres)
                {
                    ctx.GamesGenre.Remove(gameGenre);
                }
                ctx.Game.Remove(data);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Game GetById(int id)
        {
            return ctx.Game.Find(id);
        }

        public GameListVm List(string term = "", bool paging = false, int currentPage = 0)
        {
            var data = new GameListVm();

            var list = ctx.Game.ToList();


            if (!string.IsNullOrEmpty(term))
            {
                term = term.ToLower();
                list = list.Where(a => a.Title.ToLower().StartsWith(term)).ToList();
            }

            if (paging)
            {
                // here we will apply paging
                int pageSize = 5;
                int count = list.Count;
                int TotalPages = (int)Math.Ceiling(count / (double)pageSize);
                list = list.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                data.PageSize = pageSize;
                data.CurrentPage = currentPage;
                data.TotalPages = TotalPages;
            }

            foreach (var game in list)
            {
                var genres = (from genre in ctx.Genre
                              join mg in ctx.GamesGenre
                              on genre.Id equals mg.GenreId
                              where mg.GameId == game.Id
                              select genre.GenreName
                              ).ToList();
                var genreNames = string.Join(',', genres);
                game.GenreNames = genreNames;
            }
            data.GameList = list.AsQueryable();
            return data;
        }

        public bool Update(Game model)
        {
            try
            {
                // these genreIds are not selected by users and still present is gameGenre table corresponding to
                // this gameId. So these ids should be removed.
                var genresToDeleted = ctx.GamesGenre.Where(a => a.GameId == model.Id && !model.Genres.Contains(a.GenreId)).ToList();
                foreach (var mGenre in genresToDeleted)
                {
                    ctx.GamesGenre.Remove(mGenre);
                }
                foreach (int genId in model.Genres)
                {
                    var gameGenre = ctx.GamesGenre.FirstOrDefault(a => a.GameId == model.Id && a.GenreId == genId);
                    if (gameGenre == null)
                    {
                        gameGenre = new GamesGenre { GenreId = genId, GameId = model.Id };
                        ctx.GamesGenre.Add(gameGenre);
                    }
                }

                ctx.Game.Update(model);
                // we have to add these genre ids in gameGenre table
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<int> GetGenreByGameId(int gameId)
        {
            var genreIds = ctx.GamesGenre.Where(a => a.GameId == gameId).Select(a => a.GenreId).ToList();
            return genreIds;
        }
    }
}
