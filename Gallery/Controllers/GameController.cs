using Gallery.Models.Domain;
using Gallery.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gallery.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        private readonly IGameService _gameService;
        private readonly IFileService _fileService;
        private readonly IGenreService _genService;
        public GameController(IGenreService genService, IGameService GameService, IFileService fileService)
        {
            _gameService = GameService;
            _fileService = fileService;
            _genService = genService;
        }
        public IActionResult Add()
        {
            var model = new Game();
            model.GenreList = _genService.List().Select(a => new SelectListItem { Text = a.GenreName, Value = a.Id.ToString() });
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(Game model)
        {
            model.GenreList = _genService.List().Select(a => new SelectListItem { Text = a.GenreName, Value = a.Id.ToString() });
            if (!ModelState.IsValid)
                return View(model);
            if (model.ImageFile != null)
            {
                var fileResult = this._fileService.SaveImage(model.ImageFile);
                if (fileResult.Item1 == 0)
                {
                    TempData["msg"] = "File could not saved";
                    return View(model);
                }
                var imageName = fileResult.Item2;
                model.Image = imageName;
            }
            var result = _gameService.Add(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(Add));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }

        public IActionResult Edit(int id)
        {
            var model = _gameService.GetById(id);
            var selectedGenres = _gameService.GetGenreByGameId(model.Id);
            MultiSelectList multiGenreList = new MultiSelectList(_genService.List(), "Id", "GenreName", selectedGenres);
            model.MultiGenreList = multiGenreList;
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Game model)
        {
            var selectedGenres = _gameService.GetGenreByGameId(model.Id);
            MultiSelectList multiGenreList = new MultiSelectList(_genService.List(), "Id", "GenreName", selectedGenres);
            model.MultiGenreList = multiGenreList;
            if (!ModelState.IsValid)
                return View(model);
            if (model.ImageFile != null)
            {
                var fileReult = this._fileService.SaveImage(model.ImageFile);
                if (fileReult.Item1 == 0)
                {
                    TempData["msg"] = "File could not saved";
                    return View(model);
                }
                var imageName = fileReult.Item2;
                model.Image = imageName;
            }
            var result = _gameService.Update(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(GameList));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }

        public IActionResult GameList()
        {
            var data = this._gameService.List();
            return View(data);
        }

        public IActionResult Delete(int id)
        {
            var result = _gameService.Delete(id);
            return RedirectToAction(nameof(GameList));
        }
    }
}
