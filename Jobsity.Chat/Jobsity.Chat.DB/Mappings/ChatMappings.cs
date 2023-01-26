using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ChatModels = Jobsity.Chat.Models;

namespace Jobsity.Chat.DB.Mappings
{
    internal class ChatMappings : JobsityEntityMappings<ChatModels.Chat>
    {
        public override void Configure(EntityTypeBuilder<ChatModels.Chat> builder)
        {
            builder.ToTable(nameof(ChatModels.Chat));

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(256);
            builder.Property(x => x.ReceiverEmail).HasMaxLength(256);
            builder.Property(x => x.ReceiverName).HasMaxLength(256);
            builder.Property(x => x.ReceiverId).HasMaxLength(256);

            builder.HasMany(x => x.Messages).WithOne(m => m.Chat);
            builder.Navigation(x => x.Messages).AutoInclude(false);

            base.Configure(builder);
        }
    }
}
