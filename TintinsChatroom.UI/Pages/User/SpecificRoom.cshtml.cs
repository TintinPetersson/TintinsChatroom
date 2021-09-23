using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TintinsChatroom.DTO.Database;
using TintinsChatroom.DTO.Models;

namespace TintinsChatroom.UI.Pages.User
{
    public class SpecificRoomModel : PageModel
    {
        private readonly SignInManager<ChatUserModel> signInManager;
        private readonly AuthDbContext context;

        public SpecificRoomModel(SignInManager<ChatUserModel> signInManager, AuthDbContext context)
        {
            this.signInManager = signInManager;
            this.context = context;
        }
        [BindProperty]
        public ChatRoomModel ChatRoom { get; set; } = new ChatRoomModel();
        [BindProperty]
        public string NewMessage { get; set; }

        public void OnGet(int id)
        {
            ChatRoom = context.ChatRoomModels.Include(c => c.ChatMessages).ThenInclude(m => m.User).FirstOrDefault(c => c.Id == id);
        }
        public async Task<IActionResult> OnPost()
        {
            if (!string.IsNullOrEmpty(NewMessage))
            {
                ChatMessageModel chatMessage = new ChatMessageModel()
                {
                    Message = NewMessage,
                    ChatRoomId = ChatRoom.Id,
                    Date = DateTime.Now,
                    User = await signInManager.UserManager.GetUserAsync(HttpContext.User)
                };
                await context.ChatMessageModels.AddAsync(chatMessage);
                await context.SaveChangesAsync();
            }
          

            return RedirectToPage("/User/SpecificRoom", new { id = ChatRoom.Id });
        }
    }
}
