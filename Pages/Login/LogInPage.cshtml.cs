using ItemRazorV1.Models;
using ItemRazorV1.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;
using System.Runtime.ConstrainedExecution;
using Microsoft.AspNetCore.Identity;

namespace ItemRazorV1.Pages.Login
{
    public class LogInPageModel : PageModel
    {

        //public static User LoggedInUser { get; set; } = null;
        private UserService _userService;

        [BindProperty]                                     public string UserName { get; set; }
        [BindProperty, DataType(DataType.Password)]        public string Password { get; set; }
        [BindProperty]                                     public string Message { get; set; }


        public LogInPageModel(UserService userService)
        {
            _userService = userService;
       
        }

        /// <summary>
        /// Til sidst skal OnPost( ) implementeres. Metoden skal først hente listen af Users via _userService og tjekke om brugeren med
        /// (UserName, Password) findes i listen. Hvis bruger findes sættes LoggedInUser til den aktuelle user og der oprettes en ny liste
        /// af Claim-objekter der initialiseres med et nyt Claim indeholdende vores brugers UserName.
        /// Dernæst oprettes et ClaimsIdentity - objekt med vores nye liste af Claims og der laves et SignIn med denne ClaimsIdentity:
        /// </summary>
        /// <returns></returns>

        public async Task<IActionResult> OnPost()
        {

            List<User> users = _userService.Users;
            foreach (User user in users)
            {

                if (UserName == user.UserName)
                {
                    var passwordHasher = new PasswordHasher<string>();
                    if (passwordHasher.VerifyHashedPassword(null, user.Password, Password) == PasswordVerificationResult.Success)
                    {
                        //if (UserName == user.UserName && Password == user.Password)
                        //{

                        //LoggedInUser = user;

                        var claims = new List<Claim> { new Claim(ClaimTypes.Name, UserName) };

                        if (UserName == "admin") claims.Add(new Claim(ClaimTypes.Role, "admin"));

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                        return RedirectToPage("/Item/GetAllItems");
                    }
                }

            }

            Message = "Invalid attempt";
            return Page();
        }

        //public void OnGet()
        //{
        //}
    }
}
