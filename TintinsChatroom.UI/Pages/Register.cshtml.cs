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

namespace TintinsChatroom.UI.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ChatUserModel> userManager;
        private readonly SignInManager<ChatUserModel> signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AuthDbContext context;
        public RegisterModel(UserManager<ChatUserModel> userManager, SignInManager<ChatUserModel> signInManager, IWebHostEnvironment webHostEnvironment, AuthDbContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
            this.context = context;
        }

        [BindProperty]
        public Register Model { get; set; }
        [BindProperty]
        public IFormFile Photo { get; set; }
        public ChatUserModel ChatUser { get; set; }
       
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
                    Email = Model.Email,
                    Image = Model.PhotoPath
                };

                ChatUser = user;

                var result = await userManager.CreateAsync(user, Model.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    if (Photo != null)
                    {
                        // Create folder
                        if (ChatUser.Image == null)
                        {
                            ChatUser.Image = "HejDu";
                        }

                        string folder = Path.Combine(_webHostEnvironment.WebRootPath, "images");

                        if (!Directory.Exists(folder))
                        {
                            Directory.CreateDirectory(folder);
                        }

                        // Delete existing photo

                        string oldFile = Path.Combine(folder, ChatUser.Image);

                        if (System.IO.File.Exists(oldFile))
                        {
                            System.IO.File.Delete(oldFile);
                        }

                        // Upload new photo

                        string uniqueFileName = String.Concat(Guid.NewGuid(), "-", ChatUser.UserName, ".jpg");

                        string newFile = Path.Combine(folder, uniqueFileName);

                        using (var fileStream = new FileStream(newFile, FileMode.Create))
                        {
                            Photo.CopyTo(fileStream);
                        }

                        // Update repo with new photopath

                        ChatUser.Image = uniqueFileName;

                        context.Users.Update(ChatUser);
                        context.SaveChanges();

                        return RedirectToPage("/Index");
                    }

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
