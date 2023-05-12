using Gallery.Models.DTO;
using Gallery.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Gallery.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private IUserAuthenticationService authService;
        public UserAuthenticationController(IUserAuthenticationService authService)
        {
            this.authService = authService;
        }
        // create(register) admin user only once
        // modify the code below to add different users (role User) and put it in url /UserAuthentication/Register to add

        //public async Task<IActionResult> Register()
        //{
        //  /*  var model = new RegistrationModel
        //    {
        //        Name = "AK56",
        //        Email = "admin@mail.com",
        //        Username = "admin",
        //        Password = "Admin@123",
        //        PasswordConfirm = "Admin@123",
        //        Role = "Admin"
        //    };*/

        //    var model = new RegistrationModel
        //    {
        //        Name = "Jax Smith",
        //        Email = "jax@mail.com",
        //        Username = "Jax",
        //        Password = "JaxSmith@123",
        //        PasswordConfirm = "Admin@123",
        //        Role = "User"
        //    };
        //    var result = await authService.RegisterAsync(model);
        //    return Ok(result.Message);
        //}


        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if(!ModelState.IsValid) 
            {
                return View(model);
            }
            var result = await authService.LoginAsync(model);
            if(result.StatusCode == 1)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["msg"] = "Acces Denied !, incorrect Username or password !";
                return RedirectToAction(nameof(Login));
            }
            
        }

        public async Task<IActionResult> Logout()
        {
            await authService.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }



    }

   
}
