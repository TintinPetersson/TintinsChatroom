using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TintinsChatroom.DTO.Models
{
    public class ChatUserModel : IdentityUser
    {
        public string Image { get; set; }
        public virtual List<ChatMessageModel> ChatMessages { get; set; }
        public virtual List<ChatRoomModel> ChatRooms { get; set; }
    }
}
