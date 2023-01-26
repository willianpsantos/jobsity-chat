using Jobsity.Chat.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ChatModels = Jobsity.Chat.Models;

namespace Jobsity.Chat.DB.Mappings
{
    internal class ChatMessageMappings : JobsityEntityMappings<ChatModels.ChatMessage>
    {
        public override void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder.ToTable(nameof(ChatModels.ChatMessage));

            builder.Property(x => x.ChatRoomId).IsRequired(false);
            builder.Property(x => x.UserId).IsRequired(false);

            builder.HasOne(x => x.Chat).WithMany(r => r.Messages);
            builder.HasOne(x => x.ChatRoom).WithMany(r => r.Messages);

            builder.Navigation(x => x.Chat).AutoInclude(false);
            builder.Navigation(x => x.ChatRoom).AutoInclude(false);

            base.Configure(builder);
        }
    }
}
