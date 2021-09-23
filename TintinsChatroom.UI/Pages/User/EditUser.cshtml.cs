using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TintinsChatroom.DTO.Database;
using TintinsChatroom.DTO.Models;

namespace TintinsChatroom.UI.Pages.User
{
    public class EditUserModel : PageModel
    {
        private readonly UserManager<ChatUserModel> userManager;
        private readonly SignInManager<ChatUserModel> signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AuthDbContext context;
        public EditUserModel(UserManager<ChatUserModel> userManager, SignInManager<ChatUserModel> signInManager, IWebHostEnvironment webHostEnvironment, AuthDbContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
            this.context = context;
        }

        [BindProperty]
        public Register Model { get; set; } = new Register();
        [BindProperty]
        public string OldPassword { get; set; }
        [BindProperty]
        public string NewPassword { get; set; }
        [BindProperty]
        public IFormFile Photo { get; set; }
        [BindProperty]
        public ChatUserModel ChatUser { get; set; } = new ChatUserModel();
        public async Task OnGet(int id)
        {
            ChatUser = await signInManager.UserManager.GetUserAsync(HttpContext.User);
        }
        public async Task<IActionResult> OnPost()
        {
            ChatUser = await signInManager.UserManager.GetUserAsync(HttpContext.User);

            ChatUser.UserName = Model.Username;

            var result = await userManager.ChangePasswordAsync(ChatUser, OldPassword, NewPassword);

            if (result.Succeeded)
            {
                await signInManager.SignInAsync(ChatUser, false);

                if (Photo != null)
                {
                    // Create Folder

                    string folder = Path.Combine(_webHostEnvironment.WebRootPath, "images");

                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }

                    // Delete Existing Photo

                    string oldFile = Path.Combine(folder, ChatUser.Image);

                    if (System.IO.File.Exists(oldFile))
                    {
                        System.IO.File.Delete(oldFile);
                    }

                    // Upload New Photo

                    string uniqueFileName = String.Concat(Guid.NewGuid().ToString(), "-", ChatUser.UserName, ".jpg");

                    string newFile = Path.Combine(folder, uniqueFileName);

                    using (var fileStream = new FileStream(newFile, FileMode.Create))
                    {
                        Photo.CopyTo(fileStream);
                    }

                    //Update Repo with new photopath

                    ChatUser.Image = uniqueFileName;

                    context.Users.Update(ChatUser);
                    await context.SaveChangesAsync();

                    return RedirectToPage("/Index");
                }
                return RedirectToPage("/Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }


            return RedirectToPage("/Index");
        }
    }
}
