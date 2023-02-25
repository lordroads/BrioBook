using Brio.Database.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Brio.Database.DAL;

public class BrioDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<ConfirmId> ConfirmIds { get; set; }


    public BrioDbContext(DbContextOptions options) : base(options)
    {
    }

}