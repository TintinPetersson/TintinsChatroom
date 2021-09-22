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
    public class SpecificRoomModel : PageModel
    {
        private readonly SignInManager<ChatUserModel> signInManager;
        public SpecificRoomModel(SignInManager<ChatUserModel> signInManager)
        {
            this.signInManager = signInManager;
        }



        public List<ChatUserModel> ChatUsers { get; set; } = new List<ChatUserModel>();
        public ChatUserModel ChatUser { get; set; } = new ChatUserModel();
        public AuthDbContext Context { get; set; } = new AuthDbContext();
        [BindProperty]
        public ChatRoomModel RoomModel { get; set; } = new ChatRoomModel();
        [BindProperty]
        public ChatMessageModel MessageModel { get; set; } = new ChatMessageModel();


        public void OnGet(int id)
        {
            RoomModel = Context.ChatRoomModels.Where(c => c.ChatRoomId == id).FirstOrDefault();
        }
        public async Task OnPost(int id)
        {
            MessageModel.Date = DateTime.Now;
            MessageModel.ChatRoom = Context.ChatRoomModels.Where(c => c.ChatRoomId == RoomModel.ChatRoomId).FirstOrDefault();
            MessageModel.ChatUser = await signInManager.UserManager.GetUserAsync(HttpContext.User);

            RoomModel = Context.ChatRoomModels.Where(c => c.ChatRoomId == id).FirstOrDefault();

            RoomModel.Messages.Add(MessageModel);

            Context.ChatRoomModels.Update(RoomModel);

            Context.SaveChanges();
        }
    }
}
