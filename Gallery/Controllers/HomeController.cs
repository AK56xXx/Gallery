using Microsoft.AspNetCore.Mvc;
using Gallery.Repositories.Abstract;

namespace Gallery.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGameService _gameService;
        public HomeController(IGameService gameService)
        {
            _gameService = gameService;
        }
        public IActionResult Index(string term = "", int currentPage = 1)
        {
            var games = _gameService.List(term, true, currentPage);
            return View(games);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult GameDetail(int gameId)
        {
            var game = _gameService.GetById(gameId);
            return View(game);
        }
    }
}
