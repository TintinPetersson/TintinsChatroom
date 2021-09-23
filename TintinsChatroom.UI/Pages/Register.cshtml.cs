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
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ChatUserModel> userManager;
        private readonly SignInManager<ChatUserModel> signInManager;

        [BindProperty]
        public Register Model { get; set; }

        public RegisterModel(UserManager<ChatUserModel> userManager, SignInManager<ChatUserModel> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var user = new ChatUserModel()
                {
                    UserName = Model.Username,
                    Email = Model.Email
                };

                var result = await userManager.CreateAsync(user, Model.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return RedirectToPage("/Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return Page();
        }
    }
}
