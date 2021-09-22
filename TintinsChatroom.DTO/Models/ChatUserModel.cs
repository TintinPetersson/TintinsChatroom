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
        public virtual int ChatUserId { get; set; }
        public virtual string ChatUsername { get; set; }
    }
}
