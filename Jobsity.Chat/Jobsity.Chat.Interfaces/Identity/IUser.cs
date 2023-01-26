namespace Jobsity.Chat.Interfaces.Identity
{
    public interface IUser
    {
        string? Id { get; set; }
        string? Name { get; set; }
        string? Email { get; set; }
    }
}
