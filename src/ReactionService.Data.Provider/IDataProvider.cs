using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.EFSupport.Provider;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.ReactionService.Models.Db;
using Microsoft.EntityFrameworkCore;

namespace LT.DigitalOffice.ReactionService.Data.Provider;

[AutoInject(InjectType.Scoped)]
public interface IDataProvider : IBaseDataProvider
{
  public DbSet<DbReaction> Reactions { get; set; }
  public DbSet<DbReactionsGroup> ReactionsGroups { get; set; }
}
