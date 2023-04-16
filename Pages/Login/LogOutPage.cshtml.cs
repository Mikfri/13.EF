using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ItemRazorV1.Pages.Login
{
    public class LogOutPageModel : PageModel
    {

        /// <summary>
        /// Step 10 (LogOutPage Razor pages)
        /// Opret en ny page: LogOutPage.Siden skal kun have en funktion OnGet() :
        /// public async Task<IActionResult> OnGet()
        /// {            LoginPageModel.LoggedInUser = null;
        ///             await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        ///             return RedirectToPage("/index");
        /// }
        /// 
        /// Step 11 (_Layout.cshtml)
        /// </summary>


        public async Task<IActionResult> OnGet()
        {
            //LogInPageModel.LoggedInUser = null;

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/index");
        }
   
    }
}
