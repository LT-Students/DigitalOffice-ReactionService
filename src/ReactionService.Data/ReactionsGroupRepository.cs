using LT.DigitalOffice.ReactionService.Data.Interfaces;
using LT.DigitalOffice.ReactionService.Data.Provider;
using Microsoft.EntityFrameworkCore;
using System;
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

  public Task<bool> DoesExistAsync(Guid reactionsGroupId)
  {
    return _provider.ReactionsGroups.AsNoTracking().AnyAsync(x => x.Id == reactionsGroupId);
  }
}
