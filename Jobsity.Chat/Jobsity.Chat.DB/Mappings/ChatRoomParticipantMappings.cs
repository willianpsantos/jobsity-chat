using Jobsity.Chat.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ChatModels = Jobsity.Chat.Models;

namespace Jobsity.Chat.DB.Mappings
{
    internal class ChatRoomParticipantMappings : JobsityEntityMappings<ChatModels.ChatRoomParticipant>
    {
        public override void Configure(EntityTypeBuilder<ChatRoomParticipant> builder)
        {
            builder.Property(x => x.ChatRoomId).HasMaxLength(256).IsRequired();
            builder.Property(x => x.UserId).HasMaxLength(256).IsRequired();

            builder
                .HasOne(x => x.ChatRoom)
                .WithMany(r => r.Participants);

            base.Configure(builder);
        }
    }
}
