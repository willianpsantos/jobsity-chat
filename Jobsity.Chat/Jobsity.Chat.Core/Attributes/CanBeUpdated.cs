namespace Jobsity.Chat.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class CanBeUpdated : Attribute
    {
        public bool Can { get; set; }

        public CanBeUpdated(bool can) => Can = can;
    }
}
