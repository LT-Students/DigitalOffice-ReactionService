using LT.DigitalOffice.ReactionService.Data.Interfaces;
using LT.DigitalOffice.ReactionService.Data.Provider;
using LT.DigitalOffice.ReactionService.Models.Db;
using LT.DigitalOffice.ReactionService.Models.Dto.Requests.ReactionsGroup.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LT.DigitalOffice.ReactionService.Data;

public class ReactionsGroupRepository : IReactionsGroupRepository
{
  private readonly IDataProvider _provider;

  public ReactionsGroupRepository(
    IDataProvider provider)
  {
    _provider = provider;
  }

  private IQueryable<DbReactionsGroup> CreateGetPredicates(
    GetReactionsGroupFilter filter)
  {
    IQueryable<DbReactionsGroup> query = _provider.ReactionsGroups.AsQueryable();

    if (!filter.IncludeDeactivated)
    {
      query = query.Where(x => x.IsActive);
    }

    if (filter.IncludeReactions)
    {
      query = query.Include(x => x.Reactions.Where(x => x.IsActive));
    }

    return query;
  }

  public async Task<Guid?> CreateAsync(DbReactionsGroup dbReactionsGroup)
  {
    if (dbReactionsGroup is null)
    {
      return null;
    }

    _provider.ReactionsGroups.Add(dbReactionsGroup);
    await _provider.SaveAsync();

    return dbReactionsGroup.Id;
  }

  public Task<DbReactionsGroup> GetAsync(GetReactionsGroupFilter filter)
  {
    return filter is null
      ? null
      : CreateGetPredicates(filter).FirstOrDefaultAsync(x => x.Id == filter.ReactionsGroupId);
  }

  public Task<bool> DoesNameExist(string name)
  {
    return _provider.ReactionsGroups.AsNoTracking().AnyAsync(x => x.Name == name);
  }

  public Task<bool> DoesExistAsync(Guid reactionsGroupId)
  {
    return _provider.ReactionsGroups.AsNoTracking().AnyAsync(x => x.Id == reactionsGroupId);
  }
}
