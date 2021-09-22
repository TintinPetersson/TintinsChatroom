using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TintinsChatroom.DTO.Database;
using TintinsChatroom.DTO.Models;

namespace TintinsChatroom.UI.Pages.User
{
    public class CreateRoomModel : PageModel
    {
        private readonly SignInManager<ChatUserModel> signInManager;
        public ChatUserModel ChatUser { get; set; }
        [BindProperty]
        public ChatRoomModel RoomModel { get; set; } = new ChatRoomModel();
        public AuthDbContext Context { get; set; } = new AuthDbContext();
        public CreateRoomModel(SignInManager<ChatUserModel> signInManager)
        {
            this.signInManager = signInManager;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
                ChatUser = await signInManager.UserManager.GetUserAsync(HttpContext.User);

                RoomModel.ChatRoomOwner = ChatUser.ChatUserId;

                Context.ChatRoomModels.Add(RoomModel);

                Context.SaveChanges();

            return RedirectToPage("/User/ViewRooms");
        }
    }
}
