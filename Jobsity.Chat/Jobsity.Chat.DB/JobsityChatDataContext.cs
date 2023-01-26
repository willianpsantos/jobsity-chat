using Jobsity.Chat.DB.Mappings;
using Microsoft.EntityFrameworkCore;
using ChatModels = Jobsity.Chat.Models;

namespace Jobsity.Chat.DB
{
    public class JobsityChatDataContext : DbContext
    {
        public JobsityChatDataContext(DbContextOptions<JobsityChatDataContext> options) : base(options) 
        { 

        }

        public DbSet<ChatModels.Chat> Chat { get; set; }
        public DbSet<ChatModels.ChatRoom> ChatRoom { get; set; }
        public DbSet<ChatModels.ChatRoomParticipant> ChatRoomParticipant { get; set; }
        public DbSet<ChatModels.ChatMessage> ChatMessage { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ChatMappings());
            modelBuilder.ApplyConfiguration(new ChatMessageMappings());
            modelBuilder.ApplyConfiguration(new ChatRoomMappings());
            modelBuilder.ApplyConfiguration(new ChatRoomParticipantMappings());
        }
    }
}