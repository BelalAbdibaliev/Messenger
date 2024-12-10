using Messenger.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace Messenger.Data;

public class MessengerDbContext : IdentityDbContext<AppUser>
{
    public MessengerDbContext(DbContextOptions<MessengerDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Message> Messages { get; set; }
}