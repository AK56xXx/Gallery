using Gallery.Models.Domain;
using Gallery.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gallery.Controllers
{
    [Authorize]
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;
        public GenreController(IGenreService genreService)
        {
            this._genreService = genreService;
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Genre model)
        {
            if(!ModelState.IsValid)
                return View(model); 
            var result = _genreService.Add(model);
            if (result)
            {
                TempData["msg"] = "Added Succesfully !";
                return RedirectToAction(nameof(Add));
            }
            else
            {
                TempData["msg"] = "Error on server side !";
                return View();
            }
            
        }

        public IActionResult Edit(int id)
        {
            var data = _genreService.GetById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Update(Genre model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = _genreService.Update(model);
            if (result)
            {
                TempData["msg"] = "Updated Succesfully !";
                return RedirectToAction(nameof(GenreList));
            }
            else
            {
                TempData["msg"] = "Error on server side !";
                return View(model);
            }

        }

        public IActionResult GenreList()
        {
            var data = _genreService.List();
            return View(data);
        }


 
        public IActionResult Delete(int id)
        {        
            var result = _genreService.Delete(id);
            return RedirectToAction(nameof(GenreList));    
        }



    }
}
