using Jobsity.Chat.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ChatModels = Jobsity.Chat.Models;

namespace Jobsity.Chat.DB.Mappings
{
    internal class ChatRoomMappings : JobsityEntityMappings<ChatModels.ChatRoom>
    {
        public override void Configure(EntityTypeBuilder<ChatRoom> builder)
        {
            builder.ToTable(nameof(ChatModels.ChatRoom));

            builder
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(256);

            base.Configure(builder);
        }
    }
}
