using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Jobsity.Chat.Identity.Data
{
    public class IdentityDataContext : IdentityDbContext<JobsityIdentityUser>
    {
        public IdentityDataContext(DbContextOptions<IdentityDataContext> options) : base(options) 
        { 
        }
    }
}
