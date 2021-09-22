using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TintinsChatroom.DTO.Database;
using TintinsChatroom.DTO.Models;

namespace TintinsChatroom.UI.Pages.User
{
    public class ViewRoomsModel : PageModel
    {
        public AuthDbContext Context { get; set; } = new AuthDbContext();
        [BindProperty]
        public List<ChatRoomModel> RoomModels { get; set; }
        public void OnGet()
        {
            RoomModels = Context.ChatRoomModels.ToList();
        }
    }
}
