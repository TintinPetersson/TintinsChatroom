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
    public class ViewRoomsModel : PageModel
    {
        public List<ChatUserModel> ChatUser { get; set; } = new List<ChatUserModel>();
        public AuthDbContext Context { get; set; } = new AuthDbContext();
        public List<ChatRoomModel> RoomModels { get; set; } = new List<ChatRoomModel>();
        public void OnGet()
        {
            RoomModels = Context.ChatRoomModels.ToList();
        }
    }
}
