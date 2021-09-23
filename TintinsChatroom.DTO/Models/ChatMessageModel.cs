using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TintinsChatroom.DTO.Models
{
    public class ChatMessageModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public int ChatRoomId { get; set; }
        public virtual ChatRoomModel ChatRoom { get; set; } // Don't forget .Include(x => x.ChatRoom)
        public virtual ChatUserModel User { get; set; } // Don't forget .Include(x => x.ChatUser)
    }
}
