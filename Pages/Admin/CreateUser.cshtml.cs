using ItemRazorV1.Models;
using ItemRazorV1.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ItemRazorV1.Pages.Admin
{
    [Authorize(Roles = "admin")]
    public class CreateUserModel : PageModel
    {
        /// <summary>
        /// Step 1 (Pages/Admin/CreateUserPage - Razor Page)
        /// Opret en ny mappe Admin under mappen Pages og opret en ny Razor Page CreateUser i mappen Admin.
        /// 
        /// Step 2 (CreateUser.cshtml.cs)
        /// Først skal vi sikre siden mod ikke-autoriseret adgang. Det gøres ved kun at tillade brugere med rollen "admin" adgang.
        /// 
        /// Step 3 (CreateUser.cshtml)
        /// </summary>

        private UserService _userService;
        private PasswordHasher<string> passwordHasher;

        [BindProperty]                                     public string UserName { get; set; }
        [BindProperty, DataType(DataType.Password)]        public string Password { get; set; }

        public CreateUserModel(UserService userService)
        {
            _userService = userService;
            passwordHasher = new PasswordHasher<string>();
        }


        /// <summary>
        /// Til sidst skal OnPost( ) implementeres - der skal tilføjes en ny User ved at kalde AddUser(new User(UserName, Password)) på _userService
        /// Bemærk: Ignorer at AddUser( ) ikke er implementeret endnu i UserService, det sker i step 4!
        /// </summary>

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _userService.AddUser(new User(UserName, passwordHasher.HashPassword(null, Password)));
            //_userService.AddUser(new User(UserName, Password));   //<-- Gammel kode, før encryption via HashPassword
            return RedirectToPage("/Item/GetAllItems");
        }



        public void OnGet()
        {
        }
    }
}
