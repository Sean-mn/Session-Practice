using Microsoft.EntityFrameworkCore;
using SessionServer.Models;

namespace SessionServer.DBContexts;

public class UserDBContext : DbContext
{
    public UserDBContext(DbContextOptions<UserDBContext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }
}