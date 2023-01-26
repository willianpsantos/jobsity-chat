namespace Jobsity.Chat.Models
{
    public class JobsityModel
    {
        public virtual Guid Id { get; set; }
        
        public virtual Guid CreatedBy { get; set; }
        public virtual string? CreatedByName { get; set; }
        public virtual DateTimeOffset CreatedAt { get; set; }

        public virtual Guid? UpdatedBy { get; set; }
        public virtual string? UpdatedByName { get; set; }
        public virtual DateTimeOffset? UpdatedAt { get; set; }
    }
}
