using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TintinsChatroom.DTO.Models;

namespace TintinsChatroom.UI.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<ChatUserModel> signInManager;

        public LogoutModel(SignInManager<ChatUserModel> signInManager)
        {
            this.signInManager = signInManager;
        }
        public async Task<IActionResult> OnGet()
        {
            await signInManager.SignOutAsync();
            return RedirectToPage("Login");
        }
    }
}
