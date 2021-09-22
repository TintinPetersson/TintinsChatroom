using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TintinsChatroom.DTO.Models
{
    public class ChatMessageModel
    {
        [Key]
        public virtual int ChatMessageId { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual string Message { get; set; }
        public virtual int ChatRoomId { get; set; } // Foreign key - what room was this posted in?
        public virtual ChatRoomModel ChatRoom { get; set; } // Don't forget .Include(x => x.ChatRoom)
        public virtual int ChatUserId { get; set; } // Foreign key - what user wrote this message?
        public virtual ChatUserModel ChatUser { get; set; } // Don't forget .Include(x => x.ChatUser)
    }
}
