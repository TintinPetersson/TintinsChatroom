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
    public class IndexModel : PageModel
    {
        //private readonly SignInManager<ChatUserModel> signInManager;

        //public ChatUserModel ChatUser { get; set; }
        //public bool IsSignedIn { get; set; }
        //public IndexModel(SignInManager<ChatUserModel> signInManager)
        //{
        //    this.signInManager = signInManager;
        //}

        //public async Task OnGet()
        //{
        //    IsSignedIn = signInManager.IsSignedIn(HttpContext.User);
        //    if (IsSignedIn)
        //    {
        //        ChatUser = await signInManager.UserManager.GetUserAsync(HttpContext.User);
        //    }
        //}
    }
}
