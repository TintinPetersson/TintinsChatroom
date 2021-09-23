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
        public int Id { get; set; }
        public string ChatRoomName { get; set; }
        public virtual ChatUserModel Owner { get; set; }
        public virtual List<ChatMessageModel> ChatMessages { get; set; }

    }
}
