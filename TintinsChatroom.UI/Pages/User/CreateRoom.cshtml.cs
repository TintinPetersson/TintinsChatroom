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
        private readonly SignInManager<ChatUserModel> _signInManager;
        private readonly AuthDbContext _context;
        public ChatUserModel ChatUser { get; set; }
        public bool IsSignedIn { get; set; }

        [BindProperty]
        public ChatRoomModel ChatRoom { get; set; } = new ChatRoomModel();
        public CreateRoomModel(SignInManager<ChatUserModel> signInManager, AuthDbContext context)
        {
            _signInManager = signInManager;
            _context = context;
        }
        public async Task OnGet()
        {
            IsSignedIn = _signInManager.IsSignedIn(HttpContext.User);
            if (IsSignedIn)
            {
                ChatUser = await _signInManager.UserManager.GetUserAsync(HttpContext.User);
            }
        }
        public async Task<IActionResult> OnPost()
        {
            ChatUserModel user = await _signInManager.UserManager.GetUserAsync(HttpContext.User);
            ChatRoom.Owner = user;
            ChatRoom.ChatMessages = new List<ChatMessageModel>();

            await _context.ChatRoomModels.AddAsync(ChatRoom);
            await _context.SaveChangesAsync();

            return RedirectToPage("/User/ViewRooms");
        }
    }
}
