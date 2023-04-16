using ItemRazorV1.Pages.Login;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ItemRazorV1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Step 12 (Index.cshtml.cs)
        /// Når applikationen startes kan der ligge gamle login-cookies.
        /// Derfor skal vi lige sikre os at den tidligere user er "signed out". 
        /// Det gøres ved at tilføje følgende kode til OnGet() metoden i Index.cshtml.cs
        /// </summary>


        /// Nu har vi udkommenteret da formårlet før var at teste forskellige brugere og det var
        /// langsommeligt altid at sq logge ud. Nu bruges standard cookie timeout så man fortsat er logged in for en tid..


        public void OnGet()
        {
            //if (LogInPageModel.LoggedInUser == null)
            //{
            //    HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //}
        }

        //public void OnGet()
        //{

        //}
    }
}