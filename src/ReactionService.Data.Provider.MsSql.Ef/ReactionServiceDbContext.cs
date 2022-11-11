using LT.DigitalOffice.Kernel.EFSupport.Provider;
using LT.DigitalOffice.ReactionService.Models.Db;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading.Tasks;

namespace LT.DigitalOffice.ReactionService.Data.Provider.MsSql.Ef;

public class ReactionServiceDbContext : DbContext, IDataProvider
{
  public DbSet<DbReaction> Reactions { get; set; }
  public DbSet<DbReactionsGroup> ReactionsGroups { get; set; }

  public ReactionServiceDbContext(DbContextOptions<ReactionServiceDbContext> options)
    : base(options)
  {
  }

  // Fluent API is written here.
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("LT.DigitalOffice.ReactionService.Models.Db"));
  }

  public object MakeEntityDetached(object obj)
  {
    Entry(obj).State = EntityState.Detached;
    return Entry(obj).State;
  }

  async Task IBaseDataProvider.SaveAsync()
  {
    await SaveChangesAsync();
  }

  void IBaseDataProvider.Save()
  {
    SaveChanges();
  }

  public void EnsureDeleted()
  {
    Database.EnsureDeleted();
  }

  public bool IsInMemory()
  {
    return Database.IsInMemory();
  }
}
