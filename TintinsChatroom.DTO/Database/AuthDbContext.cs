using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TintinsChatroom.DTO.Models;

namespace TintinsChatroom.DTO.Database
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) :base(options)
        {

        }
        public AuthDbContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AuthDbChatroom;Trusted_Connection=True");
                optionsBuilder.UseLazyLoadingProxies();
            }
        }
        public virtual DbSet<ChatMessageModel> ChatMessageModels { get; set; }
        public virtual DbSet<ChatRoomModel> ChatRoomModels { get; set; }
        public virtual DbSet<ChatUserModel> ChatUserModels { get; set; }
    }
}
