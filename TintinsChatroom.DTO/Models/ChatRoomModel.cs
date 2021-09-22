using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TintinsChatroom.DTO.Models
{
    public class ChatRoomModel
    {
        [Key]
        public virtual int ChatRoomId { get; set; }
        public virtual int ChatRoomOwner { get; set; } // A ChatUserModel id
        public virtual string ChatRoomName { get; set; }
        public virtual List<ChatMessageModel> Messages { get; set; }

    }
}
